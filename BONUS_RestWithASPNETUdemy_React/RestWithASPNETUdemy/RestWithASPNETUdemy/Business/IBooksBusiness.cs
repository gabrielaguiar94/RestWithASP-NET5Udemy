using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Utils;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IBooksBusiness
    {
        BooksVO Create(BooksVO book);
        BooksVO FindByID(long id);
        List<BooksVO> FindAll();
        PagedSearchVO<BooksVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        BooksVO Update(BooksVO book);
        void Delete(long id);
    }
}
