using Microsoft.Extensions.Logging;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business
{
    public class ContactRepository : IContactUsRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<ContactRepository> _logger;
        public ContactRepository(ApplicationDbContext db, ILogger<ContactRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Add contact us model
        /// </summary>
        /// <param name="contactUs">contact us</param>
        /// <returns></returns>
        public ContactUs Add(ContactUs contactUs)
        {
            try
            {
                _db.ContactUs.Add(contactUs);
                var contactAddes = _db.SaveChanges();
                _logger.LogInformation("new contact was sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return contactUs;
        }

        /// <summary>
        /// get all contact us objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContactUs> Get()
        {
           return _db.ContactUs.ToList();
        }
    }
}
