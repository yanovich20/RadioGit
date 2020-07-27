using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IRepository<T> where T:TEntity
    {
        void Delete(long Id);
        void Delete(T entity);
        void SoftDelete(T entity);
        void SoftDelete(long id);
        void Insert(T entity);
        void Edit(T entity);
        List<T> All();
        T Get(long id);
        void SaveChanges();
    }
}
