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
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MailKit.Net.Imap;
using MailKit;
using Xamarin.Forms;
using MailKit.Security;

[assembly: Dependency(typeof(Xam.Forms.MailKit.Droid.MailService))]
namespace Xam.Forms.MailKit.Droid
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public async void InitClient()
        {
            try
            {
                MailClient = new ImapClient();
                MailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await MailClient.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.StackTrace);
            }
        }

        public async Task<List<Mail>> LoadEmails()
        {
            try
            {
                var currentFolder = MailClient.Inbox;

                if (currentFolder != null)
                {
                    // Open the folder for reading
                    await currentFolder.OpenAsync(FolderAccess.ReadOnly);

                    // Get the message summaries
                    var messageList = await currentFolder.FetchAsync(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Envelope | MessageSummaryItems.InternalDate);
                    return messageList?.Take(20).Select(m => new Mail { From = m.Envelope.From[0].Name, Subject = m.Envelope.Subject, Date = m.Envelope.Date.Value.LocalDateTime }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return new List<Mail>();
        }

        #endregion

    }
}