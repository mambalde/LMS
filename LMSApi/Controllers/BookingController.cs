using LMSDataManager.Library.DataAccess;
using LMSDataManager.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingsData _bookingData;
        public BookingController(IBookingsData bookingData)
        {
            _bookingData = bookingData;
        }

        [HttpPost]
        public void Post(BookingModel book)
        {
            _bookingData.SaveBookingRecord(book);
        }


        [Route("GetBookings")]
        [HttpGet]
        public List<BookingReportModel> Get()
        {
            return _bookingData.GetBookings();
        }

    }
}
