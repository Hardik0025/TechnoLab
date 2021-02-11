using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class PublisherRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));

        public static async Task<PublisherInfo> AddPublisherAsync(PublisherInfo publisher)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("publisher_name", publisher.publisher_name);
            parameters.Add("city", publisher.city);
            parameters.Add("state", publisher.state);
            parameters.Add("country", publisher.country);

            try
            {
                await dbConnection.ExecuteAsync("spPublisher_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return publisher;
        }

        public static async Task<PublisherInfo> DeletePublisherAsync(int publisherId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", publisherId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spPublisher_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<PublisherInfo>> GetPublisherAsync()
        {
            IEnumerable<PublisherInfo> publishers;
            using IDbConnection dbConnection = ConnData;

            try
            {
                publishers = await dbConnection.QueryAsync<PublisherInfo>("spPublisher_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return publishers;
        }

        public static async Task<PublisherInfo> GetPublisherByIdAsync(int publisherId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", publisherId, DbType.Int32);

            PublisherInfo publisher;

            try
            {
                publisher = await dbConnection.QueryFirstOrDefaultAsync<PublisherInfo>("spPublisher_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return publisher;
        }

        public static async Task<PublisherInfo> UpdatePublisherAsync(PublisherInfo updatedPublisher)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedPublisher.pub_id);
            parameters.Add("publisher_name", updatedPublisher.publisher_name);
            parameters.Add("city", updatedPublisher.city);
            parameters.Add("state", updatedPublisher.state);
            parameters.Add("country", updatedPublisher.country);

            try
            {
                await dbConnection.ExecuteAsync("spPublisher_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedPublisher;
        }
    }
}
