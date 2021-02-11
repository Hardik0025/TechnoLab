using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class BookPublisherRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));

        public static async Task<BookPublisher> AddBookPublisherAsync(BookPublisher bookPublisher)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("title", bookPublisher.title);
            parameters.Add("type", bookPublisher.type);
            parameters.Add("pub_id", bookPublisher.pub_id);
            parameters.Add("price", bookPublisher.price);
            parameters.Add("advance", bookPublisher.advance);
            parameters.Add("royalty", bookPublisher.royalty);
            parameters.Add("ytd_sales", bookPublisher.ytd_sales);
            parameters.Add("notes", bookPublisher.notes);
            parameters.Add("published_date", bookPublisher.published_date);
            parameters.Add("publisher_name", bookPublisher.publisher_name);
            parameters.Add("city", bookPublisher.city);
            parameters.Add("state", bookPublisher.state);
            parameters.Add("country", bookPublisher.country);

            try
            {
                await dbConnection.ExecuteAsync("spJoins_InsertBookPublisher", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return bookPublisher;
        }

        public static async Task<BookPublisher> DeleteBookPublisherAsync(string bookPublisherId, int publisherId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Book_Id", bookPublisherId);
            parameters.Add("Publisher_Id", publisherId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spJoins_DeleteBookPublisher", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<BookPublisher>> GetBookPublisherAsync()
        {
            IEnumerable<BookPublisher> bookPublishers;
            using IDbConnection dbConnection = ConnData;

            try
            {
                bookPublishers = await dbConnection.QueryAsync<BookPublisher>("spJoins_GetBookPublisher", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if(dbConnection.State == ConnectionState.Open) { dbConnection.Close(); }
            }
            return bookPublishers;
        }

        public static async Task<BookPublisher> GetBookPublisherByIdAsync(string bookId, int publisherId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Book_Id", bookId);
            parameters.Add("Publisher_Id", publisherId, DbType.Int32);

            BookPublisher bookPublisher;

            try
            {
                bookPublisher = await dbConnection.QueryFirstOrDefaultAsync<BookPublisher>("spJoins_GetOneBookPublisher", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return bookPublisher;
        }

        public static async Task<BookPublisher> UpdateBookPublisherAsync(BookPublisher updatedBookPublisher)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Book_Id", updatedBookPublisher.book_id);
            parameters.Add("title", updatedBookPublisher.title);
            parameters.Add("type", updatedBookPublisher.type);
            parameters.Add("price", updatedBookPublisher.price);
            parameters.Add("advance", updatedBookPublisher.advance);
            parameters.Add("royalty", updatedBookPublisher.royalty);
            parameters.Add("ytd_sales", updatedBookPublisher.ytd_sales);
            parameters.Add("notes", updatedBookPublisher.notes);
            parameters.Add("published_date", updatedBookPublisher.published_date);
            parameters.Add("Publisher_Id", updatedBookPublisher.pub_id);
            parameters.Add("publisher_name", updatedBookPublisher.publisher_name);
            parameters.Add("city", updatedBookPublisher.city);
            parameters.Add("state", updatedBookPublisher.state);
            parameters.Add("country", updatedBookPublisher.country);

            try
            {
                await dbConnection.ExecuteAsync("spJoins_UpdateBookPublisher", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedBookPublisher;
        }
    }
}
