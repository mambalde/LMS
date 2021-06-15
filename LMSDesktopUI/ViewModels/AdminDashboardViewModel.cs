using AutoMapper;
using Caliburn.Micro;
using LMSDesktopUI.Library.API;
using LMSDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesktopUI.ViewModels
{
    public class AdminDashboardViewModel:Screen
    {
        private readonly IBookEndpoint _bookEndpoint;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IMapper _mapper;
        public AdminDashboardViewModel(IBookEndpoint bookEndpoint, IUserEndpoint userEndpoint, IMapper mapper)
        {
            _bookEndpoint = bookEndpoint;
            _userEndpoint = userEndpoint;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {

                await LoadBooks();
            }
            catch (Exception)
            {
                await TryCloseAsync();

            }
        }

        private BindingList<BooksDisplayModel> _books = new BindingList<BooksDisplayModel>();

        public BindingList<BooksDisplayModel> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                NotifyOfPropertyChange(() => Books);
            }
        }

        private BindingList<UserDisplayModel> _users = new BindingList<UserDisplayModel>();
        public BindingList<UserDisplayModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
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

        private async Task LoadBooks()
        {
            var books = await _bookEndpoint.GetAll();
            var gottenBooks = _mapper.Map<List<BooksDisplayModel>>(books);
            Books = new BindingList<BooksDisplayModel>(gottenBooks); 

            var users = await _userEndpoint.GetAll();
            var gottenUsers = _mapper.Map<List<UserDisplayModel>>(users);
            Users = new BindingList<UserDisplayModel>(gottenUsers);

        }
    }
}
