using LMSDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public interface IBookingsEndpoint
    {
        Task PostBook(BookingModel book);
        Task<List<BookingReportModel>> GetAll();
    }
}