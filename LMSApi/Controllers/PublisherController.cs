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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherData _publisherData;
        public PublisherController(IPublisherData publisherData)
        {
            _publisherData = publisherData;
        }


        [Route("GetPublishers")]
        [HttpGet]
        public List<PublisherModel> Get()
        {
            return _publisherData.GetPublisers();
        }
    }
}
