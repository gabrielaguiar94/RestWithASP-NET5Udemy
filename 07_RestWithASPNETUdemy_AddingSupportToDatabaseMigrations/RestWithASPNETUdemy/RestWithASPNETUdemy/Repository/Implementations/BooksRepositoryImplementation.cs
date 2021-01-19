using RestWithASPNETUdemy.Model;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BooksRepositoryImplementation : IBooksRepository
    {
        private MySqlContext _context;

        public BooksRepositoryImplementation(MySqlContext context)
        {
            _context = context;

        }

        public List<Books> FindAll()
        {
            return _context.Books.ToList();
        }

        public Books FindByID(long id)
        {
            return _context.Books.SingleOrDefault((System.Linq.Expressions.Expression<Func<Books, bool>>)(b => (bool)b.Id.Equals(id)));
        }

        public Books Create(Books book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
            return book;
        }
        public Books Update(Books book)
        {
            if (!Exists(book.Id))
            {
                return null;
            }

            var result = _context.Books.SingleOrDefault((System.Linq.Expressions.Expression<Func<Books, bool>>)(b => (bool)b.Id.Equals((long)book.Id)));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return book;
        }
        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault((System.Linq.Expressions.Expression<Func<Books, bool>>)(b => (bool)b.Id.Equals(id)));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
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
            return _context.Books.Any((System.Linq.Expressions.Expression<Func<Books, bool>>)(b => (bool)b.Id.Equals(id)));
        }
    }
}
