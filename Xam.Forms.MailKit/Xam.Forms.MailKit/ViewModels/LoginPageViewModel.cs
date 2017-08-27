using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using IPageDialogService = Prism.Services.IPageDialogService;
using System;
using System.Collections.Generic;
using System.Linq;
using Xam.Forms.MailKit.Interface;
using Xamarin.Forms;

namespace Xam.Forms.MailKit.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Fields

        INavigationService _navigationService;
        IPageDialogService _dialogService;

        #endregion

        #region Constructor

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            _navigationService = navigationService;
            _dialogService = pageDialogService;
        }

        #endregion

        #region Properties

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private IMailService _mailService;
        public IMailService MailService { get { return _mailService ?? (_mailService = DependencyService.Get<IMailService>()); } }

        #endregion

        #region Commands

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(OnLogin).ObservesProperty(() => CanExecute));

        private async void OnLogin()
        {
            IsBusy = true;
            MailService.InitClient();
            bool authenticated = await MailService.AuthenticateAccount(UserName, Password);
            if (authenticated)
            {
                var param = new NavigationParameters();
                param.Add("mailService", MailService);
                await _navigationService.NavigateAsync("MailsPage", param);
                Password = string.Empty;
                UserName = string.Empty;
            }
            else
            {
                IsBusy = false;
                await _dialogService.DisplayAlertAsync("Error", "Oops, Something went wrong!", "Okay");
            }
            IsBusy = false;
        }

        #endregion
    }
}
