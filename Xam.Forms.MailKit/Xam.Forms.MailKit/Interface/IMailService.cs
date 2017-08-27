using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Forms.MailKit.Interface
{
    public interface IMailService
    {
        void InitClient();
        Task<bool> AuthenticateAccount(string userName, string password);
        Task<List<Mail>> LoadEmails();
    }
}
