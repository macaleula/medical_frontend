using MedicalApp.Logic;
using MedicalApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedicalApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(usernameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty)
            {
                
            }
            else
            {
                JWT jwt = await LoginLogic.Login(usernameEntry.Text, passwordEntry.Text);
                if (jwt.jwt == null)
                {
                    await DisplayAlert("Erro", "Usuario e/ou senha inválidos. \nTente novamente.", "Ok");
                }
                else
                {
                    await Navigation.PushAsync(new ConsultasPage());
                }
            }
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NovoUsuarioPage());
        }
    }
}
