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

        public void SaveBookRecord(BookModel book)
        {
            _sql.SaveData("dbo.spBook_Insert", book, "LMSData");
        }

        public void DeleteRecord(int Id)
        {
            
            _sql.DeleteData("dbo.spBook_Delete", new { Id }, "LMSData");
        }

        public List<BookModel> GetBooks()
        {
            var output = _sql.LoadData<BookModel, dynamic>("dbo.spBooks_GetAll", new { }, "LMSData");
            return output;
        }

    }
}
