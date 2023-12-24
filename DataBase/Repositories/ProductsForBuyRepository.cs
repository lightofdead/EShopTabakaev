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
    public class ProductsForBuyRepository : BaseRepository<ProductForBuy>
    {
        public ProductsForBuyRepository() : base(
            "ProductsForBuy",
            "[Product]",
            "[Product] = "
            ) 
        { 
        
        }
        public async Task<Guid> Create(ProductForBuy model)
        {
            return await base.Create(model, $"'{model.Product}'");
        }

        public  async Task<bool> DeleteByProductId(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [ProductsForBuy] WHERE Product = '{modelId}'";
                await db.QueryAsync<Guid>(sqlQuery, new { modelId });
                //db.Execute(sqlQuery, new { modelId });
            }
            var result = await Get(modelId) == default;
            return result;
        }
    }
}
