using System;
using System.Collections.Generic;
using System.Text;

using Core.Repositories;

namespace Core.Services.Impl
{
    public class Service<T> : IService<T> where T : TEntity
    {
        protected IRepository<T> Repository { get; set; }

        public Service(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        public List<T> All()
        {
            return Repository.All();
        }

        public virtual long Create(T entity)
        {
            long id = -1;
            try
            {
                Repository.Insert(entity);
                Repository.SaveChanges();
                return entity.Id;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public virtual bool Edit(T entity)
        {
            try {
                Repository.Edit(entity);
                Repository.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public virtual T Get(long id)
        {
            return Repository.Get(id);
        }

        public virtual bool Remove(long Id)
        {
            try {
                Repository.Delete(Id);
                Repository.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public virtual bool Remove(T entity)
        {
            try
            {
                Repository.Delete(entity);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool SoftRemove(T entity)
        {
            try
            {
                Repository.SoftDelete(entity);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual bool SoftRemove(long id)
        {
            try
            {
                Repository.SoftDelete(id);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
