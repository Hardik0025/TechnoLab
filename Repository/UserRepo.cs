using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class UserRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));
        public static async Task<User> AddUserAsync(User user)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("email_address", user.email_address);
            parameters.Add("password", user.password);
            parameters.Add("source", user.source);
            parameters.Add("first_name", user.first_name);
            parameters.Add("middle_name", user.middle_name);
            parameters.Add("last_name", user.last_name);
            parameters.Add("role_id", user.role_id);
            parameters.Add("pub_id", user.pub_id);
            parameters.Add("hire_date", user.hire_date);

            try
            {
                await dbConnection.ExecuteAsync("spUser_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return user;
        }

        public static async Task<User> DeleteUserAsync(int userId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", userId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spUser_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<User>> GetUserAsync()
        {
            IEnumerable<User> users;
            using IDbConnection dbConnection = ConnData;

            try
            {
                users = await dbConnection.QueryAsync<User>("spUser_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return users;
        }

        public static async Task<User> GetUserByIdAsync(int userId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", userId, DbType.Int32);

            User user;

            try
            {
                user = await dbConnection.QueryFirstOrDefaultAsync<User>("spUser_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return user;
        }

        public static async Task<User> UpdateUserAsync(User updatedUser)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedUser.user_id);
            parameters.Add("email_address", updatedUser.email_address);
            parameters.Add("password", updatedUser.password);
            parameters.Add("source", updatedUser.source);
            parameters.Add("first_name", updatedUser.first_name);
            parameters.Add("middle_name", updatedUser.middle_name);
            parameters.Add("last_name", updatedUser.last_name);
            parameters.Add("role_id", updatedUser.role_id);
            parameters.Add("pub_id", updatedUser.pub_id);
            parameters.Add("hire_date", updatedUser.hire_date);

            try
            {
                await dbConnection.ExecuteAsync("spUser_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedUser;
        }
    }
}
