using RestWithASPNETUdemy.Model.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Books : BaseEntity
    {

        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime LaunchedDate { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("title")]
        public string Title { get; set; }

    }
}
