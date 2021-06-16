using AutoMapper;
using Caliburn.Micro;
using LMSDesktopUI.Library.API;
using LMSDesktopUI.Library.Models;
using LMSDesktopUI.Models;
using POSDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMSDesktopUI.ViewModels
{
    public class AdminDashboardViewModel: Conductor<IScreen>
    {
        private readonly IBookEndpoint _bookEndpoint;
        private readonly IUserEndpoint _userEndpoint;
        private readonly IWindowManager _window;
        private ILoggedInUserModel _user;
        private readonly IBookingsEndpoint _bookingsEndpoint;
        private readonly IMapper _mapper;
        public AdminDashboardViewModel(IBookEndpoint bookEndpoint, IUserEndpoint userEndpoint, 
            IBookingsEndpoint bookingsEndpoint, ILoggedInUserModel user, IWindowManager window,
            IMapper mapper)
        {
            _bookEndpoint = bookEndpoint;
            _userEndpoint = userEndpoint;
            _user = user;
            _window = window;
            _bookingsEndpoint = bookingsEndpoint;
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
        private BindingList<BooksDisplayModel> _filteredBooks = new BindingList<BooksDisplayModel>();

        public BindingList<BooksDisplayModel> FilteredBooks
        {
            get { return _filteredBooks; }
            set
            {
                _filteredBooks = value;
                NotifyOfPropertyChange(() => FilteredBooks);
            }
        }

        private BindingList<BookingReportDisplayModel> _bookingReport = new BindingList<BookingReportDisplayModel>();

        public BindingList<BookingReportDisplayModel> BookingReport
        {
            get { return _bookingReport; }
            set
            {
                _bookingReport = value;
                NotifyOfPropertyChange(() => BookingReport);
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

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }
        public void Close()
        {
            TryCloseAsync();
        }

        public void AddNewBook()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.ResizeMode = ResizeMode.NoResize;
            settings.AllowsTransparency = true;
            settings.WindowStyle = WindowStyle.None;
            _window.ShowDialogAsync(IoC.Get<AddNewBookViewModel>(), null, settings);

            //await LoadBooks();
        }


        public async Task  DeleteBook()
        {
            BookModel bookToDelete = new BookModel();
            if (SelectedBook != null)
            {
                bookToDelete.Id = SelectedBook.Id;

                await _bookEndpoint.DeleteBook(bookToDelete);
                await LoadBooks();
            }
           
        }
        public void DisplayUsers()
        {

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
        private async Task LoadBooks()
        {
            Books.Clear();
            BookingReport.Clear();
            Users.Clear();
            var books = await _bookEndpoint.GetAll();
            var gottenBooks = _mapper.Map<List<BooksDisplayModel>>(books);
            Books = new BindingList<BooksDisplayModel>(gottenBooks);

            var report = await _bookingsEndpoint.GetAll();
            var gottenReport = _mapper.Map<List<BookingReportDisplayModel>>(report);
            BookingReport = new BindingList<BookingReportDisplayModel>(gottenReport);


            var newBooks = Books.Where(x => x.Availabilty == true).ToList();

            foreach (var item in newBooks)
            {
                BooksDisplayModel booksDisplay = new BooksDisplayModel
                {
                    Availabilty = item.Availabilty,
                    BookImage = item.BookImage,
                    State = item.State,
                    Category = item.Category,
                    CreatedDate = item.CreatedDate,
                    Id = item.Id,
                    Isbn = item.Isbn,
                    Price = item.Price,
                    PublisherId = item.PublisherId,
                    Title = item.Title,

                };
                FilteredBooks.Add(booksDisplay);
            }

            var users = await _userEndpoint.GetAll();
            var gottenUsers = _mapper.Map<List<UserDisplayModel>>(users);
            Users = new BindingList<UserDisplayModel>(gottenUsers);

            UserName = _user.UserName;

        }
    }
}
