using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Forms.MailKit.ViewModels;
using Xamarin.Forms;

namespace Xam.Forms.MailKit
{
    public class Mail
    {

        public string From { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string InitialName { get { return From[0].ToString().ToUpper(); } }
        public Color TextBGColor { get { return Color.FromRgb(BaseViewModel.RandomColor.Next(256), BaseViewModel.RandomColor.Next(256), BaseViewModel.RandomColor.Next(256)); } }
        Random randomColor = new Random();
    }
}
