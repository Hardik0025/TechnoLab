using Dapper;
using TechnoDapperBlazor.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository()
        {
            _connectionString = @"Data Source=DESKTOP-A3GM4EF\SQLSERVER2020;Initial Catalog=BlazorDapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public IDbConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<StudentInfo>> GetStudentsAsync()
        {
            using IDbConnection dbConnection = Connection;
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
            IEnumerable<StudentInfo> student;
            try
            {
                student = await dbConnection.QueryAsync<StudentInfo>("spStudent_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return student;
        }
        public async Task<bool> CreateStudentAsync(StudentInfo student)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("StudentName", student.StudentName);
            parameters.Add("TotalMarks", student.TotalMarks);

            using IDbConnection dbConnection = Connection;
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
            try
            {

                {
                    await dbConnection.ExecuteAsync("spStudent_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return true;
        }
        public async Task<StudentInfo> GetStudentByIdAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using IDbConnection dbConnection = Connection;
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
            StudentInfo student;
            try
            {
                student = await dbConnection.QueryFirstOrDefaultAsync<StudentInfo>("spStudent_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return student;
        }
        public async Task<bool> UpdateStudentAsync(StudentInfo updatedStudent)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedStudent.StudID, DbType.Int32);
            parameters.Add("StudentName", updatedStudent.StudentName);
            parameters.Add("TotalMarks", updatedStudent.TotalMarks);

            using IDbConnection dbConnection = Connection;
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
            try
            {
                await dbConnection.ExecuteAsync("spStudent_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return true;
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using IDbConnection dbConnection = Connection;
            if (dbConnection.State == ConnectionState.Closed)
                dbConnection.Open();
            try
            {
                await dbConnection.ExecuteAsync("spStudent_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return true;
        }
    }
}
