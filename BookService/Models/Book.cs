using System;
using System.Collections.Generic;

namespace BookService.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsCheckedOut { get; set; }
        public byte Rating { get; set; }
    }
}
