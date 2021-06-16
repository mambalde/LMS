using Caliburn.Micro;
using LMSDesktopUI.Library.API;
using LMSDesktopUI.Library.Models;
using LMSDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using AutoMapper;

namespace LMSDesktopUI.ViewModels
{
    public class AddNewBookViewModel: Screen
    {
        private readonly IBookEndpoint _bookEndpoint;
        private readonly IPublisherEndpoint _publisherEndpoint;
        private readonly IMapper _mapper;
        public AddNewBookViewModel(IBookEndpoint bookEndpoint, 
            IMapper mapper,
            IPublisherEndpoint publisherEndpoint)
        {
            _bookEndpoint = bookEndpoint;
            _mapper = mapper;
            _publisherEndpoint = publisherEndpoint;
            DisplayName = "Add New Book";
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {

                await LoadItems();
            }
            catch (Exception)
            {
                await TryCloseAsync();

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


        private BindingList<PublisherDisplayModel> _publisher = new BindingList<PublisherDisplayModel>();

        public BindingList<PublisherDisplayModel> Publisher
        {
            get { return _publisher; }
            set
            {
                _publisher = value;
                NotifyOfPropertyChange(() => Publisher);
            }
        }


        private string _bookTitle;

        public string BookTitle
        {
            get { return _bookTitle; }
            set
            {
                _bookTitle = value;
                NotifyOfPropertyChange(() => BookTitle);
            }
        }
        private string _bookState;

        public string BookState
        {
            get { return _bookState; }
            set
            {
                _bookState = value;
                NotifyOfPropertyChange(() => BookState);
            }
        }

        private string _isbn;

        public string Isbn
        {
            get { return _isbn; }
            set
            { 
                _isbn = value;
                NotifyOfPropertyChange(() => Isbn);
            }
        }

        private CategoryDisplayModel _selectedCategory;

        public CategoryDisplayModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }

        private PublisherDisplayModel _selectedPublisher;

        public PublisherDisplayModel SelectedPublisher
        {
            get { return _selectedPublisher; }
            set
            {
                _selectedPublisher = value;
                NotifyOfPropertyChange(() => SelectedPublisher);
            }
        }


        private async Task LoadItems()
        {
            string[] arrayCat = { "Romance", "Fiction", "Horror", "Fantasy", "Motivational", "History", "Travel" };

            foreach (var item in arrayCat)
            {
                CategoryDisplayModel category = new CategoryDisplayModel()
                {
                    CategoryName = item
                };
                Category.Add(category);
            }


            var publishers = await _publisherEndpoint.GetAll();
            var gottenPublishers = _mapper.Map<List<PublisherDisplayModel>>(publishers);
            Publisher = new BindingList<PublisherDisplayModel>(gottenPublishers);

        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            { 
                _imagePath = value;
                NotifyOfPropertyChange(() => ImagePath);
            }
        }

        private decimal _bookPrice;

        public decimal BookPrice
        {
            get { return _bookPrice; }
            set
            {
                _bookPrice = value;
                NotifyOfPropertyChange(() => BookPrice);
            }
        }

        public void Cancel()
        {
            TryCloseAsync();
        }

        byte[] convertImageTobytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public Image convertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                
                return Image.FromStream(ms);
            }
        }

        private Image _bookImage;

        public Image BookImage
        {
            get { return _bookImage; }
            set
            {
                _bookImage = value;
                NotifyOfPropertyChange(() => BookImage);
            }

        }

        private byte[] _imageDisplay;

        public byte[] ImageDisplay
        {
            get { return _imageDisplay; }
            set
            {
                _imageDisplay = value;
                NotifyOfPropertyChange(() => ImageDisplay);
            }
        }


        //BitmapImage src = new BitmapImage();
        //src.BeginInit();
        //src.UriSource = new Uri("image.png", UriKind.Relative);
        //src.CacheOption = BitmapCacheOption.OnLoad;
        //src.EndInit();

        //buttonImage = new Image();
        //buttonImage.Source = src;
        public void AddImage()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            if (openFile.ShowDialog()== true)
            {
                ImagePath = openFile.FileName;
                BookImage = Image.FromFile(ImagePath);
                ImageDisplay = convertImageTobytes(BookImage);
            }

        }
        public async Task Savebook()
        {
            BookModel book = new BookModel();
            book.Availabilty = true;
            book.CreatedDate = DateTime.Now;
            book.Isbn = Isbn;
            book.Price = BookPrice;
            book.PublisherId = SelectedPublisher.Id;
            book.State = BookState;
            book.Title = BookTitle;
            book.Category = SelectedCategory.CategoryName;
            book.BookImage = convertImageTobytes(BookImage);

            await _bookEndpoint.PostBook(book);


        }
    }
}
