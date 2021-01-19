using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class BooksBusinessImplementation : IBooksBusiness
    {
        private readonly IRepository<Books> _repository;

        public BooksBusinessImplementation(IRepository<Books> repository)
        {
            _repository = repository;
        }
        public List<Books> FindAll()
        {
            return _repository.FindAll();
        }
        public Books FindByID(long id)
        {
            return _repository.FindByID(id);
        }
        public Books Create(Books book)
        {
            return _repository.Create(book);
        }
        public Books Update(Books book)
        {
            return _repository.Update(book);
        }
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
