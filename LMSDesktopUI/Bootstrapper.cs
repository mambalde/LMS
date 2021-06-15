using AutoMapper;
using Caliburn.Micro;
using LMSDesktopUI.Library.API;
using LMSDesktopUI.Library.Models;
using LMSDesktopUI.Models;
using LMSDesktopUI.ViewModels;
using Microsoft.Extensions.Configuration;
using POSDesktopUI.Helpers;
using POSDesktopUI.Input;
using POSDesktopUI.Library.Api;
using POSDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LMSDesktopUI
{
    public class Bootstrapper:BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
            PasswordBoxHelper.BoundPasswordProperty,
           "Password",
           "PasswordChanged");
        }
        private IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookModel, BooksDisplayModel>();
                cfg.CreateMap<BookingReportModel, BookingReportDisplayModel>();
                cfg.CreateMap<UserModel, UserDisplayModel>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        }
        private IConfiguration Addconfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
#if DEBUG
            builder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
#else
                    builder.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
#endif
            return builder.Build();
        }
        protected override void Configure()
        {

            _container.Instance(ConfigureAutoMapper());
            _container.Instance(_container)
               .PerRequest<IUserEndpoint, UserEndpoint>()
               .PerRequest<IBookingsEndpoint, BookingsEndpoint>()
               .PerRequest<IBookEndpoint, BookEndpoint>();
            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUserModel, LoggedInUserModel>()
                .Singleton<IAPIHelper, APIHelper>();
            _container.RegisterInstance(typeof(IConfiguration), "Iconfiguration", Addconfiguration());

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));

            var defaultCreateTrigger = Parser.CreateTrigger;
            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    return defaultCreateTrigger(target, null);
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                switch (splits[0])
                {
                    case "Key":
                        var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                        return new KeyTrigger { Key = key };

                    case "Gesture":
                        var mkg = (MultiKeyGesture)(new MultiKeyGestureConverter()).ConvertFrom(splits[1]);
                        return new KeyTrigger { Modifiers = mkg.KeySequences[0].Modifiers, Key = mkg.KeySequences[0].Keys[0] };
                }

                return defaultCreateTrigger(target, triggerText);
            };
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
