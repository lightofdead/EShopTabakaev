using Interfaces.Models;

namespace Interfaces.Repositories
{
    public interface IRepository<T>
        where T : IModel
    {
        Guid Create(T model, string createString = "");
        bool Delete(Guid modelId);
        T Get(Guid modelId);
        T GetByName(string name);
        List<T> GetModels();
        void Update(T model, string updateString = "");
    }
}
