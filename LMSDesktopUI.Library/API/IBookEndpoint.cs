using LMSDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public interface IBookEndpoint
    {
        Task<List<BookModel>> GetAll();
    }
}