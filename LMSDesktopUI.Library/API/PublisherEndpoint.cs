using LMSDesktopUI.Library.Models;
using POSDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public class PublisherEndpoint : IPublisherEndpoint
    {
        private IAPIHelper _apiHelper;
        public PublisherEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }
        public async Task<List<PublisherModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Publisher/GetPublishers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<PublisherModel>>();
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
