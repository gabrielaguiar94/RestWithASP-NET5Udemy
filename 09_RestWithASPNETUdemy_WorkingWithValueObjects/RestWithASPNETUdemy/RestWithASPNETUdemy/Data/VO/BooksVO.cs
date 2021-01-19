using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BooksVO
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchedDate { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
    }
}
