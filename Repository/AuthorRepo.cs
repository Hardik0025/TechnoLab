using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class AuthorRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));
        public static async Task<Author> AddAuthorAsync(Author author)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("last_name", author.last_name);
            parameters.Add("first_name", author.first_name);
            parameters.Add("phone", author.phone);
            parameters.Add("address", author.address);
            parameters.Add("city", author.city);
            parameters.Add("state", author.state);
            parameters.Add("zip", author.zip);
            parameters.Add("email_address", author.email_address);

            try
            {
                await dbConnection.ExecuteAsync("spAuthor_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return author;
        }

        public static async Task<Author> DeleteAuthorAsync(int authorId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", authorId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spAuthor_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<Author>> GetAuthorAsync()
        {
            IEnumerable<Author> authors;
            using IDbConnection dbConnection = ConnData;

            try
            {
                authors = await dbConnection.QueryAsync<Author>("spAuthor_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return authors;
        }

        public static async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", authorId, DbType.Int32);

            Author author;

            try
            {
                author = await dbConnection.QueryFirstOrDefaultAsync<Author>("spAuthor_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return author;

        }

        public static async Task<Author> UpdateAuthorAsync(Author updatedAuthor)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedAuthor.author_id);
            parameters.Add("last_name", updatedAuthor.last_name);
            parameters.Add("first_name", updatedAuthor.first_name);
            parameters.Add("phone", updatedAuthor.phone);
            parameters.Add("address", updatedAuthor.address);
            parameters.Add("city", updatedAuthor.city);
            parameters.Add("state", updatedAuthor.state);
            parameters.Add("zip", updatedAuthor.zip);
            parameters.Add("email_address", updatedAuthor.email_address);

            try
            {
                await dbConnection.ExecuteAsync("spAuthor_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedAuthor;
        }
    }
}
