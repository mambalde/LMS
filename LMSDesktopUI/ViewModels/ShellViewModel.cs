using Caliburn.Micro;
using LMSDesktopUI.EventModels;
using POSDesktopUI.Library.Api;
using POSDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LMSDesktopUI.ViewModels
{
    public class ShellViewModel: Conductor<IScreen>.Collection.OneActive, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private ILoggedInUserModel _user;
        private IAPIHelper _apiHelper;
        public ShellViewModel(IEventAggregator events,
            IAPIHelper apiHelper,
            ILoggedInUserModel user)
        {
            _events = events;
            _user = user;
            _apiHelper = apiHelper;
            _events.SubscribeOnPublishedThread(this);
            ActivateItemAsync(IoC.Get<LoginViewModel>());
        }

        public void Close()
        {
            TryCloseAsync();
        }
        public Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(_user.UserRole )&& _user.UserRole.Equals("Admin"))
            {
                ActivateItemAsync(IoC.Get<AdminDashboardViewModel>());
            }
            else
            {
                ActivateItemAsync(IoC.Get<UserDashboardViewModel>());
            }
           
           // NotifyOfPropertyChange(() => IsLogOutVisible);
            return Task.CompletedTask;
        }
    }
}
