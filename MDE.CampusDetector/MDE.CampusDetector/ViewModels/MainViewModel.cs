using FreshMvvm;
using MDE.CampusDetector.Domain;
using MDE.CampusDetector.Domain.Models;
using MDE.CampusDetector.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MDE.CampusDetector.ViewModels
{
    public class MainViewModel: FreshBasePageModel
    {
        private readonly ICampusService campusService;

        public const string NoPermissionTitle = "Location unavailable";
        public const string NoPermissionMessage = "To allow distance measurement, please allow the app to request your locations information";
        public const string YouAreCloseMessage = "You are close to {0}";
        public const string ErrorTitle = "Error";

        public MainViewModel(ICampusService campusService)
        {
            this.campusService = campusService;

            LoadCommand = new Command(OnAppearing);
            
        }

        

        public Command LoadCommand { get; }

        public override void Init(object initData)
        {
            base.Init(initData);

            LoadCommand?.Execute(null);   
        }

        public Location LastLocation { get; set; }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }

        public bool IsCampusSelected => SelectedCampus != null;

        private Campus selectedCampus;
        public Campus SelectedCampus
        {
            get { return selectedCampus; }
            set
            {
                selectedCampus = value;
                RaisePropertyChanged(nameof(SelectedCampus));
                RaisePropertyChanged(nameof(IsCampusSelected));


                HandleLocation();
            }
        }

        private double selectedCampusDistance;
        public double SelectedCampusDistance
        {
            get { return selectedCampusDistance; }
            set
            {
                selectedCampusDistance = value;
                RaisePropertyChanged(nameof(SelectedCampusDistance));
            }
        }

        private ObservableCollection<Campus> campuses;
        public ObservableCollection<Campus> Campuses
        {
            get { return campuses; }
            set
            {
                campuses = value;
                RaisePropertyChanged(nameof(Campuses));
            }
        }

        public virtual void HandleLocation()
        {
            if (selectedCampus != null && LastLocation != null)
            {
                SelectedCampusDistance = LastLocation.CalculateDistance(
                        selectedCampus.Latitude, selectedCampus.Longitude, DistanceUnits.Kilometers);
            }
        }

        private async void OnAppearing()
        {
            try
            {
                IsLoading = true;

                //load campuses
                var campuses = await campusService.GetAllCampuses();
                Campuses = new ObservableCollection<Campus>(campuses);

                //check if location permission is granted
                bool hasPermission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
                if (!hasPermission)
                {
                    hasPermission = await Permissions.RequestAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
                }

                if (hasPermission)
                {
                    try
                    {

                        var location = await Geolocation.GetLocationAsync();
                        
                        LastLocation = location;
                        HandleLocation();
                    }
                    catch (Exception)
                    {
                        hasPermission = false;
                    }
                }
                if (!hasPermission)
                {
                    await Application.Current.MainPage.DisplayAlert(NoPermissionTitle, NoPermissionMessage, "I understand");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(ErrorTitle, ex.Message, "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
