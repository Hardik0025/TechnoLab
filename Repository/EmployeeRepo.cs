using Dapper;
using TechnoDapperBlazor.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TechnoDapperBlazor.Repository
{
    public static class EmployeeRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));
        
        public static async Task<IEnumerable<EmployeeInfo>> GetEmployeeAsync()
        {
            IEnumerable<EmployeeInfo> employees;
            using IDbConnection dbConnection = ConnData;

            try
            {
                employees = await dbConnection.QueryAsync<EmployeeInfo>("spEmployee_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return employees;
        }

        public static async Task<EmployeeInfo> GetEmployeeByIdAsync(int empId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", empId, DbType.Int32);

            EmployeeInfo employee;

            try
            {
                employee = await dbConnection.QueryFirstOrDefaultAsync<EmployeeInfo>("spEmployee_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return employee;
        }

        public static async Task<EmployeeInfo> AddEmployeeAsync(EmployeeInfo emp)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Name", emp.Name);
            parameters.Add("Department", emp.Department);
            parameters.Add("Designation", emp.Designation);
            parameters.Add("Company", emp.Company);

            try
            {
                await dbConnection.ExecuteAsync("spEmployee_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return emp;
        }

        public static async Task<EmployeeInfo> UpdateEmployeeAsync(EmployeeInfo updatedEmp)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedEmp.Id, DbType.Int32);
            parameters.Add("Name", updatedEmp.Name, DbType.String);
            parameters.Add("Department", updatedEmp.Department, DbType.String);
            parameters.Add("Designation", updatedEmp.Designation, DbType.String);
            parameters.Add("Company", updatedEmp.Company, DbType.String);

            try
            {
                await dbConnection.ExecuteAsync("spEmployee_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedEmp;
        }

        public static async Task<EmployeeInfo> DeleteEmployeeAsync(int empId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", empId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spEmployee_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }
    }
}
