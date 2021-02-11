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
    public class ProdCateSupplierRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("NorthwindDB"));

        public static async Task<List<Products>> GetProductDetailsAsync()
        {
            await using var connection = new SqlConnection(ConnData.ConnectionString);
            var sQuery = @"sp_ProdCateSupplier"; 

            await connection.OpenAsync();

            var products = await connection.QueryAsync<Products, Categories, Suppliers, Products>(sQuery, (prod, category, supply) =>
            {
                prod.Category = category;
                prod.Supplier = supply;
                return prod;
            }, splitOn: "CategoryID,SupplierID", commandType: CommandType.StoredProcedure);

            return products.ToList();
        }
        public static async Task<Products> UpdateProductAsync(Products upProducts)
        {
            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductID", upProducts.ProductID);
            parameters.Add("ProductName", upProducts.ProductName);
            parameters.Add("QuantityPerUnit", upProducts.QuantityPerUnit);
            parameters.Add("UnitPrice", upProducts.UnitPrice);
            parameters.Add("UnitsInStock", upProducts.UnitsInStock);
            parameters.Add("UnitsOnOrder", upProducts.UnitsOnOrder);
            parameters.Add("ReorderLevel", upProducts.ReorderLevel);
            parameters.Add("Discontinued", upProducts.Discontinued);
            parameters.Add("CategoryID", upProducts.Category.CategoryID);
            parameters.Add("CategoryName", upProducts.Category.CategoryName);
            parameters.Add("Description", upProducts.Category.Description);
            parameters.Add("SupplierID", upProducts.Supplier.SupplierID);
            parameters.Add("CompanyName", upProducts.Supplier.CompanyName);
            parameters.Add("ContactName", upProducts.Supplier.ContactName);
            parameters.Add("ContactTitle", upProducts.Supplier.ContactTitle);
            parameters.Add("Address", upProducts.Supplier.Address);
            parameters.Add("City", upProducts.Supplier.City);
            parameters.Add("Country", upProducts.Supplier.Country);
            parameters.Add("HomePage", upProducts.Supplier.HomePage);

            try
            {
                await sqlConnection.ExecuteAsync("sp_ProdCateSupplierUpdate", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }

            return upProducts;
            //var sQuery = @"sp_ProdCateSupplierUpdate";
            //var productUpdate = await sqlConnection.QueryAsync<Products, Categories, Suppliers, Products>(sQuery,
            //    (prod, category, supply) =>
            //    {
            //        prod.Category = category;
            //        prod.Supplier = supply;
            //        return prod;
            //    }, splitOn: "CategoryID,SupplierID", param: parameters, commandType: CommandType.StoredProcedure).ConfigureAwait(true);

        }

        public static async Task<Products> AddProductsAsync(Products products)
        {
            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductName", products.ProductName);
            parameters.Add("QuantityPerUnit", products.QuantityPerUnit);
            parameters.Add("UnitPrice", products.UnitPrice);
            parameters.Add("UnitsInStock", products.UnitsInStock);
            parameters.Add("UnitsOnOrder", products.UnitsOnOrder);
            parameters.Add("ReorderLevel", products.ReorderLevel);
            parameters.Add("Discontinued", products.Discontinued);
            parameters.Add("CategoryName", products.Category.CategoryName);
            parameters.Add("Description", products.Category.Description);
            parameters.Add("CompanyName", products.Supplier.CompanyName);
            parameters.Add("ContactName", products.Supplier.ContactName);
            parameters.Add("ContactTitle", products.Supplier.ContactTitle);
            parameters.Add("Address", products.Supplier.Address);
            parameters.Add("City", products.Supplier.City);
            parameters.Add("Country", products.Supplier.Country);
            parameters.Add("HomePage", products.Supplier.HomePage);

            try
            {
                await sqlConnection.ExecuteAsync("sp_ProdCateSupplierInsert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }
            return products;
        }
        public static async Task<Products> DeleteProductAsync(int prodId, int cateId, int supplierId, int ordId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("ProductID", prodId);
            parameters.Add("CategoryID", cateId);
            parameters.Add("SupplierID", supplierId);
            parameters.Add("OrderID",ordId);

            try
            {
                await dbConnection.ExecuteAsync("sp_ProdCateSupplierDelete", parameters, commandType: CommandType.StoredProcedure);
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
