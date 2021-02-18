using Dapper;
using TechnoDapperBlazor.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public class CustomerOrderRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("NorthwindDB"));

        public static async Task<List<CustomersDetails>> GetOrderDetailsAsync()
        {
            await using var connection = new SqlConnection(ConnData.ConnectionString);
            string sQuery =
                @"select DISTINCT c.CompanyName, c.ContactName, c.ContactTitle, c.Address, c.City, c.PostalCode, c.Phone, o.CustomerID, o.OrderDate, o.ShippedDate, o.ShipName, o.ShipCountry from CustomersDetails c inner join Orders o on c.CustomerID = o.CustomerID ";

            await connection.OpenAsync();

            var customers = await connection.QueryAsync<CustomersDetails, Orders, CustomersDetails>(sQuery, (cst, cstdtl) =>
            {
                cst.Order = cstdtl;
                return cst;
            }, splitOn: "CustomerID").ConfigureAwait(true);

            return customers.ToList();

            // await using var connection = new SqlConnection(ConnData.ConnectionString);
            // string sQuery = @" select ProductID, ProductName, p.CategoryID, CategoryName from Products p inner join Categories c on p.CategoryID = c.CategoryID ";
            // await connection.OpenAsync();

            // var products = await connection.QueryAsync<Products, Categories, Products>(sQuery, (product, category) =>
            //{
            //    product.Category = category;
            //    return product;
            //}, splitOn: "CategoryID");

            // //products.ToList().ForEach(product => Console.WriteLine($"Product: {product.ProductName}, Category: {product.Category.CategoryName}"));
            // return products.ToList();
        }
    }
}