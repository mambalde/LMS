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
    public class UserDashboardViewModel:Screen
    {
        private readonly IBookEndpoint _bookEndpoint;
        private readonly IMapper _mapper;
        public UserDashboardViewModel(IBookEndpoint bookEndpoint, IMapper mapper)
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

        private BooksDisplayModel _selectBook = new BooksDisplayModel();

        public BooksDisplayModel SelectedBook
        {
            get { return _selectBook; }
            set
            { 
                _selectBook = value;
                NotifyOfPropertyChange(() => SelectedBook);
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

        private BindingList<CategoryDisplayModel> _category = new BindingList<CategoryDisplayModel>();

        public BindingList<CategoryDisplayModel> Category
        {
            get { return _category; }
            set
            {
                _category = value;
                NotifyOfPropertyChange(() => Category);
            }
        }

        public void Close()
        {
            TryCloseAsync();
        }

        public void BorrowBook()
        {

        }

        public void ReturnBook()
        {

        }
        private async Task LoadProducts()
        {
            var books = await _bookEndpoint.GetAll();
            var gottenBooks = _mapper.Map<List<BooksDisplayModel>>(books);
            Books = new BindingList<BooksDisplayModel>(gottenBooks);

            string[] arrayCat = { "Romance", "Fiction", "Horror", "Fantasy","Motivational", "History", "Travel" };

            foreach (var item in arrayCat)
            {
                CategoryDisplayModel category = new CategoryDisplayModel()
                {
                    CategoryName = item
                };
                Category.Add(category);
            }

        }
    }
}
