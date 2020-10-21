using MedicalApp.Logic;
using MedicalApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicalApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultasPage : ContentPage
    {
        public ConsultasPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();
            var consultas = await VenueLogic.GetVenues();
            consultasListView.ItemsSource = consultas;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new NewTravelPage());
        }

        private void consultasListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}