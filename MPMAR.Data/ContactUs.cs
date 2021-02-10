using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for ContactUs table which form contactus object used in contact us screens
    /// </summary>
    public class ContactUs
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        [Required]
        public string PostalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
