using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class InfoBookById
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("ConnectionDB"));
        public static async Task<BookPublisher> GetInfoByBookAsync(string bookId, int pubId)
        {
            BookPublisher bookPublisher;
            using IDbConnection dbConnection = ConnData;

            try
            {
                bookPublisher = await dbConnection.QueryFirstOrDefaultAsync<BookPublisher>("spBookInfo", new {Book_Id = bookId, pubId }, commandType: CommandType.StoredProcedure);
            }
            
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                { dbConnection.Close(); }
            }
            return bookPublisher;
        }
        public static async Task<IEnumerable<BookPublisher>> GetDataBookAsync()
        {
            using IDbConnection dbConnection = ConnData;

            string sQuery = "select book_id, title, type, price " +
                "from dbo.Book ";
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<BookPublisher>(sQuery);
            dbConnection.Close();

            return result;
        }
    }    
}
