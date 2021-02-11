using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class BookRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));
        public static async Task<Book> AddBookAsync(Book book)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", book.title);
            parameters.Add("type", book.type);
            parameters.Add("pub_id", book.pub_id);
            parameters.Add("price", book.price);
            parameters.Add("advance", book.advance);
            parameters.Add("royalty", book.royalty);
            parameters.Add("ytd_sales", book.ytd_sales);
            parameters.Add("notes", book.notes);
            parameters.Add("published_date", book.published_date);

            try
            {
                await dbConnection.ExecuteAsync("spBook_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return book;
        }

        public static async Task<Book> DeleteBookAsync(string bookId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", bookId);

            try
            {
                await dbConnection.ExecuteAsync("spBook_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<Book>> GetBookAsync()
        {
            IEnumerable<Book> books;
            using IDbConnection dbConnection = ConnData;

            try
            {
                books = await dbConnection.QueryAsync<Book>("spBook_GetAll", commandType: CommandType.StoredProcedure);
            }
            
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return books;
        }

        public static async Task<Book> GetBookByIdAsync(int bookId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", bookId, DbType.Int32);

            Book book;

            try
            {
                book = await dbConnection.QueryFirstOrDefaultAsync<Book>("spBook_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return book;
        }

        public static async Task<Book> UpdateBookAsync(Book updatedBook)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedBook.book_id);
            parameters.Add("title", updatedBook.title);
            parameters.Add("type", updatedBook.type);
            parameters.Add("price", updatedBook.price);
            parameters.Add("advance", updatedBook.advance);
            parameters.Add("royalty", updatedBook.royalty);
            parameters.Add("ytd_sales", updatedBook.ytd_sales);
            parameters.Add("notes", updatedBook.notes);
            parameters.Add("published_date", updatedBook.published_date);

            try
            {
                await dbConnection.ExecuteAsync("spBook_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedBook;
        }
    }
}
