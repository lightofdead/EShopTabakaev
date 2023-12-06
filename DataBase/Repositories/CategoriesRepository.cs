using Dapper;
using DataBase.Models;
using Microsoft.Data.SqlClient;
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
        public Guid Create(Category model)
        {
            return base.Create(model, $"'{model.Name}'");
        }

        public void Update(Category model)
        {
            base.Update(model, $"Name = {model.Name}");
        }
    }
}
