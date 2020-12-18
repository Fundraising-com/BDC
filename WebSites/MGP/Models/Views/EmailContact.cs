using GA.BDC.Web.MGP.Helpers.Extensions;
namespace GA.BDC.Web.MGP.Models.Views
{
    public class EmailContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string EmailAddress { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAdded { get; set; }
        public bool IsModified { get; set; }
        public bool IsDeleted { get; set; }
        
        public bool IsEmpty
        {
            get { return FirstName.IsEmpty() && EmailAddress.IsEmpty(); }
        }
    }
}