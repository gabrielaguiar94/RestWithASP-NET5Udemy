using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        protected MySqlContext _context;

        private DbSet<T> dataset;

        public GenericRepository(MySqlContext context)
        {
            _context = context;
            dataset = _context.Set<T>();

        }
        public List<T> FindAll()
        {
            return dataset.ToList();
        }
        public T FindByID(long id)
        {
            return dataset.SingleOrDefault(t => t.Id.Equals(id));
        }
        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(t => t.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return null;
            }
        }
        public void Delete(long id)
        {

            var result = dataset.SingleOrDefault((System.Linq.Expressions.Expression<Func<T, bool>>)(t => (bool)t.Id.Equals(id)));
            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public bool Exists(long id)
        {
            return dataset.Any(t => (bool)t.Id.Equals(id));
        }
    }
}
