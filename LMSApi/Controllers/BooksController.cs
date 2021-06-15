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
    public class BooksController : ControllerBase
    {
        private readonly IBookData _bookData;
        public BooksController(IBookData bookData)
        {
            _bookData = bookData;
        }

        [Route("GetBooks")]
        [HttpGet]
        public List<BookModel> Get()
        {
            return _bookData.GetBooks();
        }

    }
}
