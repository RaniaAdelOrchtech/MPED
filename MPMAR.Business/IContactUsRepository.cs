using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business
{
  public  interface IContactUsRepository
    {
        /// <summary>
        /// Add contact us model
        /// </summary>
        /// <param name="contactUs">contact us</param>
        /// <returns></returns>
        public ContactUs Add(ContactUs contactUs);

        /// <summary>
        /// get all contact us objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ContactUs> Get();
    }
}
