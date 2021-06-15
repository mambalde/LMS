using LMSDataManager.Library.Models;
using System.Collections.Generic;

namespace LMSDataManager.Library.DataAccess
{
    public interface IBookingsData
    {
        void SaveBookingRecord(BookingModel book);
        List<BookingReportModel> GetBookings();
    }
}