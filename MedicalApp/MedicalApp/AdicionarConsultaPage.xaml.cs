using MedicalApp.Logic;
using MedicalApp.Model;
using Newtonsoft.Json;
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
    public partial class AdicionarConsultaPage : ContentPage
    {
        private List<Usuario> medicos;

        public AdicionarConsultaPage()
        {
            InitializeComponent();
        }

        public AdicionarConsultaPage(List<Usuario> medicos)
        {
            InitializeComponent();
            this.medicos = medicos;
            pckmedico.ItemsSource = medicos;
        }

        async public static Task<AdicionarConsultaPage> BuildAdicionarConsultaPageAsync(string municipio)
        {
            List<Usuario> medicoList = new List<Usuario>();
            medicoList = await ConsultaLogic.GetMedicoMunicipio(municipio);
            return new AdicionarConsultaPage(medicoList);
        }

        private async void NovaConsultaButton_Clicked(object sender, EventArgs e)
        {
            String dateTimeString = "";
            DateTime dateTime = DateTime.MinValue;

            dateTime = Convert.ToDateTime(_timePicker.Time.ToString());

            dateTimeString = DateTime.Parse(DataPicker.Date.ToString()).ToString("yyyy-MM-dd") + " " +
                             dateTime.ToString("HH:mm");
            dateTime = DateTime.Parse(dateTimeString);

            Usuario medico = (Usuario)pckmedico.SelectedItem;

            Consulta novaConsulta = new Consulta();
            novaConsulta.medico = medico;
            novaConsulta.paciente = LoginLogic.GetLoggedUsuario();
            novaConsulta.data = dateTimeString;

            Consulta aux = await ConsultaLogic.NovaConsulta(novaConsulta);
            await Navigation.PushAsync(new ConsultasPage());
        }

        private async void _timePicker_Unfocused(object sender, FocusEventArgs e)
        {
            Boolean accepted = false;
            TimeSpan min = new TimeSpan(9, 0, 0);
            TimeSpan max = new TimeSpan(20, 00, 0);
            TimeSpan horario = _timePicker.Time;
            if(horario.CompareTo(min) != -1)
            {
                if(horario.CompareTo(max) != 1)
                {
                    accepted = true;
                }
            }
            if(!accepted)
            {
                await DisplayAlert("Erro", "Horário inválido.", "Ok");
                _timePicker.Time = new TimeSpan(9, 0, 0);
            }
            
        }
    }
}