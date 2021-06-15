using LMSDataManager.Library.Internal.DataAccess;
using LMSDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.DataAccess
{
    public class BookData : IBookData
    {
        private readonly ISqlDataAccess _sql;

        public BookData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<BookModel> GetBooks()
        {
            var output = _sql.LoadData<BookModel, dynamic>("dbo.spBooks_GetAll", new { }, "LMSData");
            return output;
        }

    }
}
