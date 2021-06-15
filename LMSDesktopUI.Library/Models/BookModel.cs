using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDesktopUI.Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Isbn { get; set; }
        public int PubllisherId { get; set; }
        public string State { get; set; }
        public bool Availabilty { get; set; }
        public byte BookImage { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
