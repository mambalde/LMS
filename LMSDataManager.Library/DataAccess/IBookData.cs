using LMSDataManager.Library.Models;
using System.Collections.Generic;

namespace LMSDataManager.Library.DataAccess
{
    public interface IBookData
    {
        List<BookModel> GetBooks();
        void SaveBookRecord(BookModel book);
        void DeleteRecord(int Id);
    }
}