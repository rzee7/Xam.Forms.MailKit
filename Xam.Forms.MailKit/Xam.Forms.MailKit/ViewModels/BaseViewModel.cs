using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xam.Forms.MailKit.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        #region Properties

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); CanExecute = !IsBusy; }
        }

        private bool _canExecute;
        public bool CanExecute
        {
            get { return _canExecute; }
            set { SetProperty(ref _canExecute, value); }
        }

        #endregion

        #region FancyFaking
        public static Random RandomColor = new Random(); 
        #endregion
    }
}
