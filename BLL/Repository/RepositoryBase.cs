using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public abstract class RepositoryBase<T, ID> where T : class
    {
        protected internal static MyContext dbContext;

        public virtual List<T> GetAll()
        {
            try
            {
                dbContext = new MyContext();
                return dbContext.Set<T>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual T GetById(ID id)
        {
            try
            {
                dbContext = new MyContext();
                return dbContext.Set<T>().Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Insert(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual T Insert2(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Add(entity);
                dbContext.SaveChanges();
                return entity;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual void Update()
        {
            try
            {
                dbContext = dbContext ?? new MyContext();
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
