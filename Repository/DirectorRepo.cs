using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class DirectorRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));
        public static async Task<Director> AddDirectorAsync(Director director)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FirstName", director.FirstName);
            parameters.Add("FamilyName", director.FamilyName);
            parameters.Add("DoB", director.DoB);
            parameters.Add("DoD", director.DoD);
            parameters.Add("Gender", director.Gender);

            try
            {
                await dbConnection.ExecuteAsync("spDirector_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return director;
        }

        public static async Task<Director> DeleteDirectorAsync(int directorId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", directorId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spDirector_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<Director>> GetDirectorAsync()
        {
            IEnumerable<Director> directors;
            using IDbConnection dbConnection = ConnData;

            try
            {
                directors = await dbConnection.QueryAsync<Director>("spDirector_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return directors;
        }

        public static async Task<Director> GetDirectorByIdAsync(int directorId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", directorId, DbType.Int32);

            Director director;

            try
            {
                director = await dbConnection.QueryFirstOrDefaultAsync<Director>("spDirector_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return director;
        }

        public static async Task<Director> UpdateDirectorAsync(Director updatedDirector)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedDirector.DirectorID);
            parameters.Add("FirstName", updatedDirector.FirstName);
            parameters.Add("FamilyName", updatedDirector.FamilyName);
            parameters.Add("DoB", updatedDirector.DoB);
            parameters.Add("DoD", updatedDirector.DoD);
            parameters.Add("Gender", updatedDirector.Gender);

            try
            {
                await dbConnection.ExecuteAsync("spDirector_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedDirector;
        }
    }
}
