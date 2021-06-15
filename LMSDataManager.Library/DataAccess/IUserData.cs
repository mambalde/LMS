using LMSDataManager.Library.Models;
using System.Collections.Generic;

namespace LMSDataManager.Library.DataAccess
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string Id);
    }
}