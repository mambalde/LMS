using LMSDesktopUI.Library.Models;
using POSDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public class BookingsEndpoint : IBookingsEndpoint
    {
        private IAPIHelper _apiHelper;
        public BookingsEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostBook(BookingModel book)
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Booking", book))
            {

                if (response.IsSuccessStatusCode)
                {
                    //Do
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public async Task<List<BookingReportModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Booking/GetBookings"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<BookingReportModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


    }
}
