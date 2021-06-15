using System;
using System.Collections.Generic;
using System.Text;

namespace LMSDataManager.Library.Models
{
    public class ApplicationUserModel
    {
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string Email { get; set; }
        public Dictionary<string, string> Roles { get; set; } = new Dictionary<string, string>();
    }
}
