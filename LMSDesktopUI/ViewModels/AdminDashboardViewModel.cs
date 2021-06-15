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
        private readonly IMapper _mapper;
        public AdminDashboardViewModel(IBookEndpoint bookEndpoint, IMapper mapper)
        {
            _bookEndpoint = bookEndpoint;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {

                await LoadProducts();
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

        private async Task LoadProducts()
        {
            var books = await _bookEndpoint.GetAll();
            var gottenBooks = _mapper.Map<List<BooksDisplayModel>>(books);
            Books = new BindingList<BooksDisplayModel>(gottenBooks);
        }
    }
}
