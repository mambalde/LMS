﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesktopUI.ViewModels
{
    public class LoginViewModel: Screen
    {
        private string _userName = "mambalde@naver.com";
        private string _password = "Sonfonia2@";
        //private IAPIHelper _apiHelper;
        private IEventAggregator _events;
        public LoginViewModel(IEventAggregator events)
        {
            _events = events;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);

            }
        }

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;

                }
                return output;
            }

        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }


        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;

            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                //var result = await _apiHelper.Authenticate(UserName, Password);

                // capture more info about the user

               // await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
                //await _events.PublishOnUIThreadAsync(new LogOnEvent());

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }


        }
    }
}