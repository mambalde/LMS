using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LMSDesktopUI.ViewModels
{
    public class AdminDashboardViewModel:Screen
    {
        public AdminDashboardViewModel()
        {

        }

        private BindingList<string> _books;

        public BindingList<string> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                NotifyOfPropertyChange(() => Books);
            }
        }

        private BindingList<string> _users;
        public BindingList<string> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Books);
            }
        }

        public void Close()
        {
            TryCloseAsync();
        }

        public void AddNewBook()
        {

        }

        public void DisplayUsers()
        {

        }
    }
}
