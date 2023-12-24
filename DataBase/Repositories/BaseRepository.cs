using Dapper;
using DataBase.Models;
using Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataBase.Repositories
{
    public abstract class BaseRepository<T>
        where T : ModelBase
    {
        protected string connectionString = "Server=localhost\\SQLEXPRESS;Database=EShopBD;Trusted_Connection=True;encrypt=false";
        private string _tableName;
        private string _fields;
        private string _propertyForFind;

        public BaseRepository(string tableName, string fields, string propertyForFind) { 
            _fields= fields;
            _propertyForFind= propertyForFind;
            _tableName= tableName;
        }

        public BaseRepository(string tableName, string fields)
        {
            _fields = fields;
            _propertyForFind = string.Empty;
            _tableName = tableName;
        }

        public virtual async Task<Guid> Create(T model, string values)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO [{_tableName}] ({_fields}) VALUES({values})";

                 //db.QueryAsync(sqlQuery, model);
                await db.QueryAsync<T>(sqlQuery, model);
            }
            var result = model.Id;
            return result;
        }

        public virtual async Task<bool> Delete(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [{_tableName}] WHERE Id = '{modelId}'";
                await db.QueryAsync<T>(sqlQuery, new { modelId });
                //db.Execute(sqlQuery, new { modelId });
            }
            var result = Get(modelId) == default;
            return result;
        }

        public virtual async Task<T> Get(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<T>($"SELECT * FROM [{_tableName}] WHERE Id = '{modelId}'");
                return result.FirstOrDefault();
            }
        }

        public virtual async Task<List<T>> GetModelsByProperty(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<T>($"SELECT * FROM [{_tableName}]  WHERE {_propertyForFind}");
                return result.ToList();
            }
        }

        public virtual async Task<List<T>> GetModels()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<T>($"SELECT * FROM [{_tableName}]");
                return result.ToList();
            }
        }

        public virtual async void Update(T model, string values)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE {_tableName} SET {values} WHERE Id = '{model.Id}'";
                await db.QueryAsync<T>(sqlQuery, model);

                //db.Execute(sqlQuery, model);
            }
        }

        public virtual async Task<T> GetByName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = await db.QueryAsync<T>($"SELECT * FROM [{_tableName}]  WHERE Name = '{name}'");
                return result.FirstOrDefault();
            }
        }
    }
}
