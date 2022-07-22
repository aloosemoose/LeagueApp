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

namespace LeagueApp
{
    

    public partial class MainWindow : Window
    {
   
        public string ChampionInput;      
        public bool validInput;

        public MainWindow()
        {
            InitializeComponent();
            ProcessImage();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            //Save the user input, check if it's valid by passing it through GetRquest(), open the window if true
            //The Url is case sensitive e.g Aatrox, Ahri
            ChampionInput = UserInpput.Text;

            if (ChampionInput.Length == 0)
            {
                ErrorAdvice();
            }
            else
            {
                ChampionInput = ChampionInput.ToLower();
                ChampionInput = char.ToUpper(ChampionInput[0]) + ChampionInput.Substring(1);

                //Wukong's ID is "monkeyking" in API
                if (ChampionInput == "Wukong")
                {
                    ChampionInput = "MonkeyKing";
                }
                if (IsItValid() == false)
                {
                    ErrorAdvice();
                }
                else
                {
                    Window1 win2 = new Window1();
                    win2.Show();
                }
            }

           
        }

        public bool IsItValid()
        {
            Window1 win2 = new Window1();

            if (win2.GetRequest())
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public void ProcessImage()
        {
            Image imgPhoto = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ahri_0.jpg");
            bitmap.EndInit();
            Ahri.Source = bitmap;
        }

        public void ErrorAdvice()
        {
            MessageBox.Show("You must input a valid champion name. Make sure not to include any spaces or punctuation; it is not case sensitive.");
        }

    }
}


