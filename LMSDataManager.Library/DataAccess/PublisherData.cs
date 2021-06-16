using LMSDataManager.Library.Internal.DataAccess;
using LMSDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.DataAccess
{
    public class PublisherData : IPublisherData
    {
        private readonly ISqlDataAccess _sql;

        public PublisherData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<PublisherModel> GetPublisers()
        {
            var output = _sql.LoadData<PublisherModel, dynamic>("dbo.spPublisher_GetAll", new { }, "LMSData");
            return output;
        }

    }
}
