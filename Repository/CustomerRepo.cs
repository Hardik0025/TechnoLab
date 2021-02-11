using TechnoDapperBlazor.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public class CustomerRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name", customer.Name);
            parameters.Add("Age", customer.Age);
            parameters.Add("Company", customer.Company);
            parameters.Add("Email", customer.Email);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Address", customer.Address);

            try
            {
                await dbConnection.ExecuteAsync("spCustomer_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(int custId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", custId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spCustomer_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {
            IEnumerable<Customer> customers;
            using IDbConnection dbConnection = ConnData;

            try
            {
                customers = await dbConnection.QueryAsync<Customer>("spCustomer_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", customerId, DbType.Int32);

            Customer customer;

            try
            {
                customer = await dbConnection.QueryFirstOrDefaultAsync<Customer>("spCustomer_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return customer;
        }

        public async Task<Customer> UpdateCustomerAsync(Customer updatedCustomer)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedCustomer.Customer_id, DbType.Int32);
            parameters.Add("Name", updatedCustomer.Name, DbType.String);
            parameters.Add("Age", updatedCustomer.Age, DbType.Int32);
            parameters.Add("Company", updatedCustomer.Company, DbType.String);
            parameters.Add("Email", updatedCustomer.Email, DbType.String);
            parameters.Add("Phone", updatedCustomer.Phone, DbType.String);
            parameters.Add("Address", updatedCustomer.Address, DbType.String);

            try
            {
                await dbConnection.ExecuteAsync("spCustomer_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedCustomer;
        }
    }
}
