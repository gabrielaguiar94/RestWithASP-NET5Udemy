﻿using RestWithASPNETUdemy.Data.VO;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business
{
    public interface IBooksBusiness
    {
        BooksVO Create(BooksVO book);
        BooksVO FindByID(long id);
        List<BooksVO> FindAll();
        BooksVO Update(BooksVO book);
        void Delete(long id);
    }
}
