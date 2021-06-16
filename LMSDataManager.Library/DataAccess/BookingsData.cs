using LMSDataManager.Library.Internal.DataAccess;
using LMSDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.DataAccess
{
    public class BookingsData : IBookingsData
    {
        private readonly ISqlDataAccess _sql;

        public BookingsData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public void SaveBookingRecord(BookingModel book)
        {
            _sql.SaveData("dbo.spBooking_Insert", book, "LMSData");
        }


        public List<BookingReportModel> GetBookings()
        {
            var output = _sql.LoadData<BookingReportModel, dynamic>("dbo.spBookings_GetAll", new { }, "LMSData");
            return output;
        }
    }
}
