using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeSol.Repo
{
    public class IEntity
    {
    }
    public enum DBtype
    {
        SqlServerDB, MongoDB, OracleDB
    }
    public interface IRepository<T>where T : IEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(int id);
        Task<T> GetByID(int Id);

    }
}
