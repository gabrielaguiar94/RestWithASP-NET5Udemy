using System;
using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
    public class BooksVO
    {
        [JsonPropertyName("id_book")]
        public long Id { get; set; }
        public string Author { get; set; }
        [JsonIgnore]
        public DateTime LaunchedDate { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
    }
}
