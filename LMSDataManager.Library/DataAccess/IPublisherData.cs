using LMSDataManager.Library.Models;
using System.Collections.Generic;

namespace LMSDataManager.Library.DataAccess
{
    public interface IPublisherData
    {
        List<PublisherModel> GetPublisers();
    }
}