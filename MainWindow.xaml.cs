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
using System.Diagnostics;

namespace LeagueApp
{
    

    public partial class MainWindow : Window
    {
        public string userChampionInput;
        public string ChampionInput;      
        public bool validInput;
        public string ahriImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ahri_0.jpg";

        public MainWindow()
        {
            InitializeComponent();
            ProcessImage();
        }
        
        //User can input ChampionName using button or enter key
        public void Button_Click(object sender, RoutedEventArgs e)
        {                   
            ChampionInput = UserInpput.Text;
            ProcessInput();
                          
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Return)
            {

                ChampionInput = UserInpput.Text;
                userChampionInput = UserInpput.Text;

                ProcessInput();
                
            }
        }
        public void ProcessInput()
        {
            //The .json is case sensitive, some champion names have spaces and punctuation. Kha'Zix - "KhaZix", Miss Fortune - "MissFortune"


            if (PeskyChampions() == false)
            {

                if (ChampionInput.Contains(" "))
                {
                    string s = ChampionInput[0].ToString();
                    string s1 = ChampionInput[ChampionInput.Length - 1].ToString();
                    if (s == " " || s1 == " ")
                    {
                        ErrorAdvice();
                    }
                    else
                    {
                        ChampionInput = ChampionInput.Replace(" ", string.Empty);
                        ValidateAndGo();
                    }

                }
                else if (ChampionInput.Contains("'"))
                {
                    string s = ChampionInput[0].ToString();
                    string s1 = ChampionInput[ChampionInput.Length - 1].ToString();

                    if (s == "'" || s1 == "'")
                    {
                        ErrorAdvice();
                    }
                    else
                    {
                        ChampionInput = ChampionInput.ToLower();
                        ChampionInput = char.ToUpper(ChampionInput[0]) + ChampionInput.Substring(1);
                        ChampionInput = ChampionInput.Replace("'", string.Empty);
                        ValidateAndGo();
                    }

                }
                else
                {
                  ChampionInput = ChampionInput.ToLower();
                  ChampionInput = char.ToUpper(ChampionInput[0]) + ChampionInput.Substring(1);
                    ValidateAndGo();
                }
            }


        }
        public void ValidateAndGo()
        {               

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

        public void ErrorAdvice()
        {
            MessageBox.Show("You must input a valid champion name. The search is case sensitive for two part names, make sure to include the correct punctuation and spaces.");
        }

        public void ProcessImage()
        {
            //Ahri was my first main :))
            Image imgPhoto = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(ahriImage);
            bitmap.EndInit();
            Ahri.Source = bitmap;
        }

        public bool PeskyChampions()
        {
            //Wukong's ID is "monkeyking" in API
            //Nunu & Willump 

            if (ChampionInput.Length == 0)
            {
                ErrorAdvice();
                return true;
            }
            else if (ChampionInput == "Wukong")
                {
                    ChampionInput = "MonkeyKing";
                    ValidateAndGo();
                return true;
                }
            
            else if (ChampionInput == "Nunu & Willump")
            {
                ChampionInput = "Nunu";
                ValidateAndGo();
                return true;
            }
            else
            {
                return false;
            }
            
            

        }
    }
}


