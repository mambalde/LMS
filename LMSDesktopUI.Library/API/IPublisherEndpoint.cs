using LMSDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public interface IPublisherEndpoint
    {
        Task<List<PublisherModel>> GetAll();
    }
}