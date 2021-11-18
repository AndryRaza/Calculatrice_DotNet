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
using System.Windows.Shapes;

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private bool gameIsOver = false;
        private bool playerOne = true;
        private bool playerTwo = false;
        private int plays = 0;
        private int[,] arrayGame = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        private void put(Button b)
        {
            if (b.Content == null)
            {
                string name = b.Name;
                if (playerOne)
                {
                    b.Content = "X";
                    changeGameArray(1, ((int)Char.GetNumericValue(name[1]), (int)Char.GetNumericValue(name[2])));
                    lifeGame();
                }
                if (playerTwo)
                {
                    b.Content = "O";
                    changeGameArray(2, ((int)Char.GetNumericValue(name[1]), (int)Char.GetNumericValue(name[2])));
                    lifeGame();
                }
                changeTurn();
            }

        }
        private void play(object sender, RoutedEventArgs e)
        {
            if (!gameIsOver && plays < 9)
            {
                put((Button)sender);
            }
        }

        private void changeTurn()
        {
            playerOne = !playerOne;
            playerTwo = !playerTwo;
            plays++;
        }

        private void changeGameArray(int p, (int, int) coord)
        {
            int x = coord.Item1;
            int y = coord.Item2;
            arrayGame[x, y] = p;
        }

        private void lifeGame()
        {
            //Lignes
            if ((arrayGame[0, 0] == 1 && arrayGame[0, 1] == 1 && arrayGame[0, 2] == 1) || (arrayGame[0, 0] == 2 && arrayGame[0, 1] == 2 && arrayGame[0, 2] == 2))
            {
                gameIsOver = true;
            }
            if ((arrayGame[1, 0] == 1 && arrayGame[1, 1] == 1 && arrayGame[1, 2] == 1) || (arrayGame[1, 0] == 2 && arrayGame[1, 1] == 2 && arrayGame[1, 2] == 2))
            {
                gameIsOver = true;
            }
            if ((arrayGame[2, 0] == 1 && arrayGame[2, 1] == 1 && arrayGame[2, 2] == 1) || (arrayGame[2, 0] == 2 && arrayGame[2, 1] == 2 && arrayGame[2, 2] == 2))
            {
                gameIsOver = true;
            }

            //Colonnes
            if ((arrayGame[0, 0] == 1 && arrayGame[1, 0] == 1 && arrayGame[2, 0] == 1) || (arrayGame[0, 0] == 2 && arrayGame[1, 0] == 2 && arrayGame[2, 0] == 2))
            {
                gameIsOver = true;
            }
            if ((arrayGame[0, 1] == 1 && arrayGame[1, 1] == 1 && arrayGame[2, 1] == 1) || (arrayGame[0, 1] == 2 && arrayGame[1, 1] == 2 && arrayGame[2, 1] == 2))
            {
                gameIsOver = true;
            }
            if ((arrayGame[0, 2] == 1 && arrayGame[1, 2] == 1 && arrayGame[2, 2] == 1) || (arrayGame[0, 2] == 2 && arrayGame[1, 2] == 2 && arrayGame[2, 2] == 2))
            {
                gameIsOver = true;
            }

            //Diagonales
            if ((arrayGame[0, 0] == 1 && arrayGame[1, 1] == 1 && arrayGame[2, 2] == 1) || (arrayGame[0, 0] == 2 && arrayGame[1, 1] == 2 && arrayGame[2, 2] == 2))
            {
                gameIsOver = true;
            }
            if ((arrayGame[2, 0] == 1 && arrayGame[1, 1] == 1 && arrayGame[0, 2] == 1) || (arrayGame[0, 2] == 2 && arrayGame[1, 1] == 2 && arrayGame[0, 2] == 2))
            {
                gameIsOver = true;
            }

            if (gameIsOver)
            {
                MessageBox.Show("Fini");
            }

        }
    }
}
