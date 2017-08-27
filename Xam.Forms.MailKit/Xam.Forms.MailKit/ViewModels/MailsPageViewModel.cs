using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xam.Forms.MailKit.Interface;

namespace Xam.Forms.MailKit.ViewModels
{
    public class MailsPageViewModel : BaseViewModel, INavigationAware
    {
        #region Constructor

        public MailsPageViewModel()
        {

        }

        #endregion

        #region Properties
        private IMailService _mailService;

        private List<Mail> _messages;
        public List<Mail> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        #endregion

        #region Commands

        private DelegateCommand _loadMailsCommand;
        public DelegateCommand LoadMailsCommand =>
            _loadMailsCommand ?? (_loadMailsCommand = new DelegateCommand(OnLoadMails));

        private async void OnLoadMails()
        {
            IsBusy = true;
            var msg = await _mailService?.LoadEmails();
            Messages = msg;
            IsBusy = false;
        }

        #endregion

        #region INavigationAware: Implementations

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //Access MailService
            _mailService = parameters["mailService"] as IMailService;
            LoadMailsCommand.Execute();
        }

        #endregion
    }
}
