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
    public partial class NovoUsuarioPage : ContentPage
    {
        public NovoUsuarioPage()
        {
            InitializeComponent();
            List<string> tiposUsuario = new List<string>();
            tiposUsuario.Add("Médico");
            tiposUsuario.Add("Paciente");
            Dictionary<int, string> valuess = new Dictionary<int, string>();
            valuess.Add(1, "Médico");
            valuess.Add(2, "Paciente");
            TipoUsuarioPicker.ItemsSource = tiposUsuario;

        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            bool isUsernameEmpty = string.IsNullOrEmpty(usernameEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);
            bool isNomeEmpty = string.IsNullOrEmpty(nomeEntry.Text);
            bool isTipoUsuarioEmpty = false;
            if (TipoUsuarioPicker.SelectedIndex != 1 && TipoUsuarioPicker.SelectedIndex != 2)
            {
                isTipoUsuarioEmpty = true;
            }


            if (isUsernameEmpty || isPasswordEmpty || isNomeEmpty || isTipoUsuarioEmpty)
            {

            }
            else
            {
                int tipoUsuarioId = TipoUsuarioPicker.SelectedIndex + 1;
                Usuario usuario = new Usuario();
                usuario.nome = nomeEntry.Text;
                usuario.username = usernameEntry.Text;
                usuario.password = passwordEntry.Text;
                usuario.tipoUsuario = TipoUsuario.GetInstance(tipoUsuarioId);


                Console.WriteLine("MEIO");


                Usuario result = await LoginLogic.Register(usuario);
                if (result != null)
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possível cadastrar o novo usuário.", "Ok");
                }

                Console.WriteLine("FIM");

            }
        }
    }
}