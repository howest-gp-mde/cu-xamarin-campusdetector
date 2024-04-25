using FreshMvvm;
using MDE.CampusDetector.Domain.Services;
using MDE.CampusDetector.Domain.Services.Api;
using MDE.CampusDetector.Pages;
using MDE.CampusDetector.ViewModels;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MDE.CampusDetector
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<ICampusService, ApiCampusService>();
            FreshIOC.Container.Register<HttpClient>(new HttpClient());

            MainPage = FreshPageModelResolver.ResolvePageModel<MainViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
