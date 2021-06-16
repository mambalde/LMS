using AutoMapper;
using Caliburn.Micro;
using LMSDesktopUI.Library.API;
using LMSDesktopUI.Library.Models;
using LMSDesktopUI.Models;
using POSDesktopUI.Library.Api;
using POSDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace LMSDesktopUI.ViewModels
{
    public class UserDashboardViewModel:Screen
    {
        private readonly IBookEndpoint _bookEndpoint;
        private readonly IBookingsEndpoint _bookingsEndpoint;
        private ILoggedInUserModel _user;
        private readonly IMapper _mapper;
        public UserDashboardViewModel(IBookEndpoint bookEndpoint, IMapper mapper,
            ILoggedInUserModel user,
            IBookingsEndpoint bookingsEndpoint)
        {
            _bookEndpoint = bookEndpoint;
            _bookingsEndpoint = bookingsEndpoint;
            _user = user;
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

        private BooksDisplayModel _selectBook = new BooksDisplayModel();

        public BooksDisplayModel SelectedBook
        {
            get { return _selectBook; }
            set
            { 
                _selectBook = value;
                NotifyOfPropertyChange(() => SelectedBook);
                NotifyOfPropertyChange(() => CanBorrowBook);
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

        private BindingList<BookingReportDisplayModel> _bookingReports = new BindingList<BookingReportDisplayModel>();

        public BindingList<BookingReportDisplayModel> BookingReports
        {
            get { return _bookingReports; }
            set
            {
                _bookingReports = value;
                NotifyOfPropertyChange(() => BookingReports);
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


        public bool CanBorrowBook
        {
            get
            {
                bool output = false;


                if (SelectedBook != null)
                {
                    output = true;
                }

                return output;
            }
        }
        public bool CanReturnBook
        {
            get
            {
                bool output = false;


                if (SelectedBook != null)
                {
                    output = true;
                }

                return output;
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

        public async Task BorrowBook()
        {
            if (SelectedBook != null)
            {
                BookingModel book = new BookingModel();
                book.BookId = SelectedBook.Id;
                book.UserId = _user.Id;
                book.BookedDate = DateTime.Now;
                await _bookingsEndpoint.PostBook(book);
            }

            await LoadBooks();
            
        }

        public void ReturnBook()
        {

        }
        private async Task LoadBooks()
        {
            var books = await _bookEndpoint.GetAll();
            var gottenBooks = _mapper.Map<List<BooksDisplayModel>>(books);
            Books = new BindingList<BooksDisplayModel>(gottenBooks);
            
            var report = await _bookingsEndpoint.GetAll();
            var gottenReport = _mapper.Map<List<BookingReportDisplayModel>>(report);
            BookingReports = new BindingList<BookingReportDisplayModel>(gottenReport);

            BookingReport.Clear();
            var newReport = BookingReports.Where(x => x.UserId == _user.Id).ToList();

            foreach (var item in newReport)
            {
                BookingReportDisplayModel booking = new BookingReportDisplayModel
                {
                    StaffName = item.StaffName,
                    BookedDate = item.BookedDate,
                    Title = item.Title
                };

                BookingReport.Add(booking);
            }
            

            string[] arrayCat = { "Romance", "Fiction", "Horror", "Fantasy","Motivational", "History", "Travel" };

            foreach (var item in arrayCat)
            {
                CategoryDisplayModel category = new CategoryDisplayModel()
                {
                    CategoryName = item
                };
                Category.Add(category);
            }

            UserName = _user.UserName;

        }
    }
}
