/*
MailKit is Copyright (C) 2013-2016 Xamarin Inc. and is licensed under the MIT license:
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xam.Forms.MailKit.Interface;
using MailKit.Net.Imap;
using MailKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(MailService))]
namespace Xam.Forms.MailKit.iOS
{
    public class MailService : Interface.IMailService
    {
        #region Fields

        ImapClient MailClient;

        #endregion
        
        #region IMailService: Implementations

        public async Task<bool> AuthenticateAccount(string userName, string password)
        {
            try
            {
                MailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                await MailClient.AuthenticateAsync(userName, password);
                return true; //This is just an assumption.
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void InitClient()
        {
            MailClient = new ImapClient();
        }

        public async Task<List<Mail>> LoadEmails()
        {
            var currentFolder = MailClient.GetFolder(MailClient.PersonalNamespaces[0]);

            if (currentFolder != null)
            {
                // Open the folder for reading
                await currentFolder.OpenAsync(FolderAccess.ReadOnly);

                // Get the message summaries
                var messageList = await currentFolder.FetchAsync(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.InternalDate);
                return messageList?.Select(m => new Mail { From = m.Envelope.From.ToString() }).ToList();
            }
            return new List<Mail>();
        }

        #endregion
    }
}