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
    }
}
