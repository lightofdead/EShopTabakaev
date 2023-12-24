using Dapper;
using DataBase.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories
{
    public class CategoriesRepository : BaseRepository<Category>
    {
        public CategoriesRepository() : base(
            "Categories",
            "[Name]",
            "Name = "
            ) 
        { 
        
        }
        public async Task<Guid> Create(Category model)
        {
            return await base.Create(model, $"'{model.Name}'");
        }

        public async void Update(Category model)
        {
            base.Update(model, $"Name = '{model.Name}'");
        }

        public async Task<List<Product>> GetProducts(string categoryName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = @"SELECT p.ID, p.Name, p.Url, p.Description, p.Count, p.Price, p.CategoryID, c.Name, c.Number
                FROM Products p 
                INNER JOIN Categories c ON p.CategoryID = c.ID 
                LEFT JOIN ProductsForBuy PFB ON p.Id = PFB.Product "+
                $"WHERE c.Name = '{categoryName}' and PFB.Product is NULL";

                var products = await connection.QueryAsync<Product, Category, Product>(sql, (product, category) => {
                    product.Category = category;
                    return product;
                },
                splitOn: "CategoryID");
                var result = products.ToList();
                return result;
            }
        }
    }
}
