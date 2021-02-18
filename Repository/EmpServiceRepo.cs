using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using TechnoDapperBlazor.Models;

namespace TechnoDapperBlazor.Repository
{
    public class EmpServiceRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConService"));

        public static async Task<IEnumerable<EmpService>> GetEmployeeAsync()
        {
            IEnumerable<EmpService> employees;
            using IDbConnection dbConnection = ConnData;

            try
            {
                employees = await dbConnection.QueryAsync<EmpService>("spGetEmployees", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return employees;
        }

        public static async Task<EmpService> GetEmployeeByIdAsync(int empId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmpId", empId, DbType.Int32);

            EmpService employee;

            try
            {
                employee = await dbConnection.QueryFirstOrDefaultAsync<EmpService>("spEmployee_GetById", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return employee;
        }

        public static bool AddEmployee(EmpService emp)
        {
            bool isValid = true;

            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FirstName", emp.FirstName);
            parameters.Add("LastName", emp.LastName);
            parameters.Add("EmailAddress", emp.EmailAddress);
            parameters.Add("PhoneNumber", emp.PhoneNumber);
            parameters.Add("Position", emp.Position);
            parameters.Add("Department", emp.Department);
            parameters.Add("StartDate", emp.StartDate);
            parameters.Add("Shift", emp.Shift);
            parameters.Add("Manager", emp.Manager);
            parameters.Add("FavoriteColor", emp.FavoriteColor);
            //parameters.Add("Photo", emp.Photo);
            try
            {
                 dbConnection.ExecuteAsync("spEmployee_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                isValid = false;
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return isValid;
        }

        public static bool UpdateEmployee(EmpService updatedEmp)
        {
            bool isValid = true;
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmpId", updatedEmp.EmpId);
            parameters.Add("FirstName", updatedEmp.FirstName);
            parameters.Add("LastName", updatedEmp.LastName);
            parameters.Add("EmailAddress", updatedEmp.EmailAddress);
            parameters.Add("PhoneNumber", updatedEmp.PhoneNumber);
            parameters.Add("Position", updatedEmp.Position);
            parameters.Add("Department", updatedEmp.Department);
            parameters.Add("StartDate", updatedEmp.StartDate);
            parameters.Add("Shift", updatedEmp.Shift);
            parameters.Add("Manager", updatedEmp.Manager);
            parameters.Add("FavoriteColor", updatedEmp.FavoriteColor);

            if (updatedEmp.TerminationDate == new DateTime(0001, 1, 1))
            {
                parameters.Add("TerminationDate");
            }
            else
            {
                parameters.Add("TerminationDate", updatedEmp.TerminationDate);
            }

            try
            {
                dbConnection.ExecuteAsync("spEmployee_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                isValid = false;
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return isValid;
        }

        public static async Task<EmpService> DeleteEmployeeAsync(int empId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("EmpId", empId, DbType.Int32);

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
        public static async Task<List<string>> ReportHiredEmployeeAsync()
        {
            await using SqlConnection dbConnection = new SqlConnection(ConnData.ConnectionString);
            string sQuery = @"select COUNT(*) from Employees where DATEDIFF(DAY,StartDate, GETDATE()) between 0 and 7 ";

            await dbConnection.OpenAsync();

            var result = await dbConnection.QueryAsync<string>(sQuery);
            await dbConnection.CloseAsync();

            return result.ToList();
        }
        public static async Task<List<string>> ReportTerminatedEmployeeAsync()
        {
            await using SqlConnection dbConnection = new SqlConnection(ConnData.ConnectionString);
            string sQuery = @"select COUNT(*) from Employees where DATEDIFF(YEAR,TerminationDate, GETDATE()) between 0 and 1 ";

            await dbConnection.OpenAsync();

            var result = await dbConnection.QueryAsync<string>(sQuery);
            await dbConnection.CloseAsync();

            return result.ToList();
        }
    }
}