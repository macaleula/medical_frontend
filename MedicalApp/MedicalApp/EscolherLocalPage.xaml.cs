using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MedicalApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EscolherLocalPage : ContentPage
    {
        private bool hasLocationPermission = false;
        private Plugin.Geolocator.Abstractions.Position mainPosition;

        public EscolherLocalPage()
        {
            InitializeComponent();
            GetPermission();
        }

        private async void GetPermission()
        {
            try
            {
#pragma warning disable CS0618 // Type or member is obsolete
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse);
#pragma warning restore CS0618 // Type or member is obsolete

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need your location.", "We need to access your location.", "Ok");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse))
                    {
                        status = results[Permission.LocationWhenInUse];
                    }

                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    hasLocationPermission = true;
                    locationsMap.IsShowingUser = true;

                    GetLocation();
                }
                else
                {
                    await DisplayAlert("Location Denied", "The Permission was denied.", "Ok");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }

            GetLocation();
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            if (hasLocationPermission)
            {
                var placemark = await ReverseGeocodeLocationAsync(convertPosition(mainPosition));
                await Navigation.PushAsync(await AdicionarConsultaPage.BuildAdicionarConsultaPageAsync(placemark.SubAdminArea));
            } else
            {

            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void GetLocation()
        {
            if (hasLocationPermission)
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                mainPosition = position;
                MoveMap(position);
            }
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }

        private Xamarin.Forms.Maps.Position convertPosition(Plugin.Geolocator.Abstractions.Position position)
        {
            return new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
        }

        async Task<Placemark> ReverseGeocodeLocationAsync(Xamarin.Forms.Maps.Position position)
        {
            try
            {

                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);

                var placemark = placemarks?.FirstOrDefault();
                return placemark;
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
            return null;
        }
    }
}