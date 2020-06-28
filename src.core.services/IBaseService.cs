using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using src.app.shared.entities;

namespace src.core.services
{
    public interface IBaseService<T>
    where T: class, IEntity
    {
        Task<List<T>> FindAll();
        Task<T> Find(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
    }
}
