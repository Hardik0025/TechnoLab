using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class StoreRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));
        public static async Task<Store> AddStoreAsync(Store store)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("store_name", store.store_name);
            parameters.Add("store_address", store.store_address);
            parameters.Add("city", store.city);
            parameters.Add("state", store.state);
            parameters.Add("zip", store.zip);

            try
            {
                await dbConnection.ExecuteAsync("spStore_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return store;
        }

        public static async Task<Store> DeleteStoreAsync(int storeId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", storeId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spStore_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<Store>> GetStoreAsync()
        {
            IEnumerable<Store>  stores;
            using IDbConnection dbConnection = ConnData;

            try
            {
                stores = await dbConnection.QueryAsync<Store>("spStore_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return stores;
        }

        public static async Task<Store> GetStoreByIdAsync(int storeId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", storeId, DbType.Int32);

            Store store;

            try
            {
                store = await dbConnection.QueryFirstOrDefaultAsync<Store>("spStore_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return store;
        }

        public static async Task<Store> UpdateStoreAsync(Store updatedStore)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedStore.store_id);
            parameters.Add("store_name", updatedStore.store_name);
            parameters.Add("store_address", updatedStore.store_address);
            parameters.Add("city", updatedStore.city);
            parameters.Add("state", updatedStore.state);
            parameters.Add("zip", updatedStore.zip);

            try
            {
                await dbConnection.ExecuteAsync("spStore_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedStore;
        }
    }
}
