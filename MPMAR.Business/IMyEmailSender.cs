using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business
{
   public interface IMyEmailSender
    {
        Task<bool> SendEmailAsync(string toEmail, string toUserName, string subject, string message);
    }
}
