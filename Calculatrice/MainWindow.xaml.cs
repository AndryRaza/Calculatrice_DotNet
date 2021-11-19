using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Net.Http.Json;

namespace Calculatrice
{
    /// <summary>
    /// floateraction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Key
    {
        public string row_id { get; set; }
        public string api { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        
        }

 
        //Pour pouvoir taper un autre nombre après avoir cliqué sur un opérateur, si c'est faux pas de clique opérateur sinon vrai
        private Boolean afterOperation = false;

        //Liste des opérations
        private List<string> listOperations = new List<string>();

        //Savoir le signe
        private Boolean minusSigne = false;

        private string? licenceKey = null;
        private bool licenceOk = false;

        private static readonly HttpClient client = new HttpClient();

        private void updateNumber(string? n)
        {
         

            if(screenResult.Text.Length < 14)
            {
                if(minusSigne)
                {
                    n = "-" + n;
                    changeSigne();
                }

                if (screenResult.Text == "0" && n != ",")
                {
                    screenResult.Text = n;
                }
                else
                {
                    screenResult.Text += n;
                }

                if (afterOperation)
                {
                    screenResult.Text = n;
                    changeAfterOperation();
                }

                EasterEgg();
            }

        }

        private void reset()
        {
            screenResult.Text = "0";
        }

        //Pour taper un nouveau chiffre après avoir cliqué sur un des boutons d'opération
        private void changeAfterOperation()
        {
            afterOperation = !afterOperation;
        }

        private void changeSigne()
        {
            minusSigne = !minusSigne;
        }

        private void button_click(object sender, RoutedEventArgs e)
        {

            if(licenceOk)
            {
                if ((Button)sender == button_comma)
                {
                    updateNumber(",");
                }

                if ((Button)sender == button_negative)
                {
                    changeSigne();
                }

                if ((Button)sender == button_0)
                {
                    if (screenResult.Text != "0")
                    {
                        updateNumber("0");
                    }
                }

                if ((Button)sender == button_1)
                {
                    updateNumber("1");
                }

                if ((Button)sender == button_2)
                {
                    updateNumber("2");
                }

                if ((Button)sender == button_3)
                {
                    updateNumber("3");
                }

                if ((Button)sender == button_4)
                {
                    updateNumber("4");
                }

                if ((Button)sender == button_5)
                {
                    updateNumber("5");
                }

                if ((Button)sender == button_6)
                {
                    updateNumber("6");
                }
                if ((Button)sender == button_7)
                {
                    updateNumber("7");
                }

                if ((Button)sender == button_8)
                {
                    updateNumber("8");
                }

                if ((Button)sender == button_9)
                {
                    updateNumber("9");
                }

                if ((Button)sender == button_plus)
                {
                    operation("+");
                }

                if ((Button)sender == button_minus)
                {
                    operation("-");
                }

                if ((Button)sender == button_divide)
                {
                    operation("/");
                }

                if ((Button)sender == button_x)
                {
                    operation("*");
                }

                if ((Button)sender == button_equal)
                {
                    result();
                }
            }
            else
            {
                read();
            }



        }
        private void operation(string op)
        {
            listOperations.Add(screenResult.Text);
            float provi = 0;
    
            if( listOperations.Count == 3)
            {
                if (listOperations[1] == "+")
                {
                    provi = float.Parse(listOperations[0])  + float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "-")
                {
                    provi = float.Parse(listOperations[0]) - float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "*")
                {
                    provi = float.Parse(listOperations[0]) * float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "/")
                {
                    provi = float.Parse(listOperations[0]) / float.Parse(listOperations[2]);
                }

                listOperations.Clear();
                listOperations.Add(provi.ToString());
                screenResult.Text = provi.ToString();
                changeAfterOperation();
            }
            else
            {
                reset();

            }
            listOperations.Add(op);

        }

        private void result()
        {
            listOperations.Add(screenResult.Text);
            float provi = 0;

            if (listOperations.Count == 3)
            {
                Console.WriteLine(listOperations);
                if (listOperations[1] == "+")
                {
                    provi = float.Parse(listOperations[0]) + float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "-")
                {
                    provi = float.Parse(listOperations[0]) - float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "*")
                {
                    provi = float.Parse(listOperations[0]) * float.Parse(listOperations[2]);
                }
                if (listOperations[1] == "/")
                {
                    provi = float.Parse(listOperations[0]) / float.Parse(listOperations[2]);
                }

                screenResult.Text = provi.ToString();
                listOperations.Clear();
                changeAfterOperation();
            }
            else
            {
                listOperations.Clear();
            }
        }

        //Bouton C
        private void button_clear_Click(object sender, RoutedEventArgs e)
        {
            afterOperation = false;
            screenResult.Text = "0";
            listOperations.Clear();
            minusSigne = false;
        }

        //Bouton CE qui efface seulement un chiffre
        private void button_clearOne_Click(object sender, RoutedEventArgs e)
        {
            if(!afterOperation && screenResult.Text.Length > 1)
            {
                screenResult.Text = screenResult.Text.Remove(screenResult.Text.Length - 1);
            }

            else
            if (!afterOperation && screenResult.Text.Length == 1)
            {
                screenResult.Text = "0";
            }

        }
        //Lire le fichier où se trouve la clé puis la vérifier, en cas d'erreur ouvre une fenetre pour entrer une clé
        private void read()
        {
            try
            {
                StreamReader sw = new StreamReader("licence.txt");
                licenceKey = sw.ReadLine();
                sw.Close();
                ValidateLicencekey(licenceKey);

            }
            catch(System.IO.FileNotFoundException)
            {
                var window = new Validationlicence();
                window.ShowDialog();
            }

        }

        //Valider la clé 
        private async Task ValidateLicencekey(string l)
        {
            string txt = "";
            HttpResponseMessage response = await client.GetAsync("https://v1.nocodeapi.com/andry974/google_sheets/qIxGfcybupTYjQnU/search?tabId=api-licencecalc&searchKey=licence&searchValue=" + l);
            if (response.IsSuccessStatusCode)
            {
                txt = await response.Content.ReadAsStringAsync();
                if (txt == "[]")
                {
                    var window = new Validationlicence();
                    window.ShowDialog();
                }
                else
                {
                    licenceOk = true;
                }
            }
        }

        private  void EasterEgg()
        {
            if(screenResult.Text == "707")
            {
                var window = new Window1();
                window.Show();
            }
        }

        //private static async Task getData()
        //{
        //    string txt = "";
        //    HttpResponseMessage response = await client.GetAsync("https://v1.nocodeapi.com/andry974/google_sheets/qIxGfcybupTYjQnU?tabId=api-database");
        //    var test = await client.GetFromJsonAsync<Key>("https://v1.nocodeapi.com/andry974/google_sheets/qIxGfcybupTYjQnU?tabId=api-database");
        //    MessageBox.Show(test.ToString());
        //    if (response.IsSuccessStatusCode)
        //    {
        //        txt = await response.Content.ReadAsStringAsync();
        //        var serializeOptions = new JsonSerializerOptions
        //        {
        //            WriteIndented = true,
        //            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        //        };
        //        string json = JsonSerializer.Serialize(txt,serializeOptions);
        //    }


        //}
    }
}
