using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivdcSupportModel
{
    /// <summary>
    /// login information to display for the user
    /// </summary>
    public class LoginInformation
    {
        public string DomainName { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public bool LoginStatus { get; set; }
        public string StatusMessage { get; set; }
        public string LoginLogoutIcon { get; set; }
    }
}