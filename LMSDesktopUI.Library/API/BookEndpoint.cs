using LMSDesktopUI.Library.Models;
using POSDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesktopUI.Library.API
{
    public class BookEndpoint : IBookEndpoint
    {
        private IAPIHelper _apiHelper;
        public BookEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;

        }

        public async Task<List<BookModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Books/GetBooks"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<BookModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task PostBook(BookModel book)
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Books", book))
            {

                if (response.IsSuccessStatusCode)
                {
                    //Do
                }
                else
                {
                    // impliment Logging here
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
        public async Task DeleteBook(BookModel book)
        {

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Books/DeleteBook", book))
            {

                if (response.IsSuccessStatusCode)
                {
                    //Do
                }
                else
                {
                    // impliment Logging here
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
