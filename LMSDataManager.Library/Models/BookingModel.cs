using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.Models
{
    public class BookingModel
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime BookedDate { get; set; }
    }
}
