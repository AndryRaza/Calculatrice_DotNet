using System.Windows;
using System.IO;
using System.Net.Http;


namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour Validationlicence.xaml
    /// </summary>
    public partial class Validationlicence : Window
    {

        private static readonly HttpClient client = new HttpClient();
        public Validationlicence()
        {
            InitializeComponent();
        }

        private async void validate_Click(object sender, RoutedEventArgs e)
        {
            string licence = licencekey.Text;
            HttpResponseMessage response = await client.GetAsync("https://v1.nocodeapi.com/andry974/google_sheets/qIxGfcybupTYjQnU/search?tabId=api-licencecalc&searchKey=licence&searchValue=" + licence);
            if (response.IsSuccessStatusCode)
            {
                string txt = await response.Content.ReadAsStringAsync();
                if(txt == "[]")
                {
                    MessageBox.Show("La licence est invalide.");
                }
                else
                {
                    save(licence);
                    this.Close();
                }
    
            }
        }
        private void save(string licence)
        {
            StreamWriter sw = new StreamWriter("licence.txt");
            sw.WriteLine(licence);
            sw.Close();
        }

    }
}
