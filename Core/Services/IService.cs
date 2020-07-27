using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IService<T> where T: TEntity
    {
        bool Remove(long Id);
        bool Remove(T entity);
        bool SoftRemove(T entity);
        bool SoftRemove(long id);
        long Create(T entity);
        bool Edit(T entity);
        List<T> All();
        T Get(long id);
    }
}
