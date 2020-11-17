using MedicalApp.CustomComponents;
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
            var consultas = await ConsultaLogic.GetConsultas();
            consultasListView.ItemsSource = consultas;
            Console.WriteLine(LoginLogic.GetLoggedUsuario().tipoUsuario.nome);
            if (LoginLogic.GetLoggedUsuario().tipoUsuario.nome == "Paciente")
            {
                ToolbarItems.Clear();
                HideableToolbarItem item = new HideableToolbarItem();
                item.Text = "NOVA CONSULTA";
                item.Clicked += ToolbarItem_Clicked;
                ToolbarItems.Add(item);
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new AdicionarConsultaPage());
            Navigation.PushAsync(new EscolherLocalPage());

        }

        private void consultasListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}