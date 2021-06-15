using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDesktopUI.Models
{
    public class BookingReportDisplayModel
    {
        public int BookingId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public DateTime BookedDate { get; set; }
        public string Title { get; set; }
        public string StaffName { get; set; }
    }
}
