using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xam.Forms.MailKit.Interface;
using Xamarin.Forms;

namespace Xam.Forms.MailKit.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        

        public MainPageViewModel()
        {
        }

        #region Methods


        #endregion
    }
}
