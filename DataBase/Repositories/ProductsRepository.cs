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
    public class ProductsRepository : BaseRepository<Product>
    {
        public ProductsRepository() : base(
            "Products",
            "[Name], [Price], [Count], [Description], [Url], [CategoryId]",
            "[CategoryId] = "
            ) 
        { 
        
        }
        public async Task<Guid> Create(Product model)
        {
            return await base.Create(model, $"'{model.Name}', '{model.Price}', '{model.Count}', '{model.Description}', '{model.Url}', '{model.CategoryId}'");
        }

        public async void Update(Product model)
        {
            base.Update(model, $"Name = '{model.Name}'");
        }
    }
}
