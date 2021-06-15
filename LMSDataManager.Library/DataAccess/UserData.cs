
using LMSDataManager.Library.Internal.DataAccess;
using LMSDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;
        
        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<UserModel> GetUserById(string Id)
        {
            var output = _sql.LoadData<UserModel, dynamic>("dbo.spUser_Lookup", new { Id }, "LMSData");
            return output;
        }

    }
}
