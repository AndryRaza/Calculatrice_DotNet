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

namespace Calculatrice
{
    /// <summary>
    /// floateraction logic for MainWindow.xaml
    /// </summary>
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

             
            }

        }

        private void reset()
        {
            screenResult.Text = "0";
        }

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
                if(screenResult.Text != "0")
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

        private void button_clear_Click(object sender, RoutedEventArgs e)
        {

            afterOperation = false;
            screenResult.Text = "0";
            listOperations.Clear();
            minusSigne = false;
        }
    }
}
