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

        public Guid Create(T model, string values)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO [{_tableName}] ({_fields}) VALUES({values})";

                db.Execute(sqlQuery, model);
            }
            var result = model.Id;
            return result;
        }

        public bool Delete(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM [{_tableName}] WHERE Id = '{modelId}'";
                db.Execute(sqlQuery, new { modelId });
            }
            var result = Get(modelId) == default;
            return result;
        }

        public T Get(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = db.Query<T>($"SELECT * FROM [{_tableName}] WHERE Id = '{modelId}'").FirstOrDefault();
                return result;
            }
        }

        public List<T> GetModelsByProperty(Guid modelId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = db.Query<T>($"SELECT * FROM [{_tableName}]  WHERE {_propertyForFind}").ToList();
                return result;
            }
        }

        public List<T> GetModels()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = db.Query<T>($"SELECT * FROM [{_tableName}]").ToList();
                return result;
            }
        }

        public void Update(T model, string values)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"UPDATE {_tableName} SET {values} WHERE Id = '{model.Id}'";

                db.Execute(sqlQuery, model);
            }
        }

        public T GetByName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var result = db.Query<T>($"SELECT * FROM [{_tableName}]  WHERE Name = '{name}'").FirstOrDefault();
                return result;
            }
        }
    }
}
