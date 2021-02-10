using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.ViewModels
{
    public class ContactUsViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
 
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
 
        public string PostalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; } 
    }
}
