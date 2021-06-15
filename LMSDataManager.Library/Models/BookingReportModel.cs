using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.Models
{
    public class BookingReportModel
    {
        public int BookingId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime BookedDate { get; set; }
        public string Title { get; set; }
        public string StaffName { get; set; }
      
    }
}
