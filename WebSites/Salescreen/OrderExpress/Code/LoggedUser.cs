using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QSP.OrderExpress.Web.Code
{

    [Serializable]
    public class LoggedUser
    {
        public int UserTypeId { get; set; }
        public string UserTypeDescription { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FMId { get; set; }
    }
}
