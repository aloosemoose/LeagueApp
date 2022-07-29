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
using RestSharp;
using Newtonsoft.Json;


namespace LeagueApp
{
   
    public partial class MainWindow : Window
    {
        public string BaseUrl = "https://ddragon.leagueoflegends.com";
        public int errorMessage = 0;
        public string userChampionInput;
        public string ChampionInput;      
        public bool validInput;
        public string ahriImage = "https://ddragon.leagueoflegends.com/cdn/img/champion/splash/Ahri_0.jpg";

        public MainWindow()
        {
          
            InitializeComponent();         
            ProcessImage();
        }
 
        
        //User can input ChampionInput using button or enter key

        public void Button_Click(object sender, RoutedEventArgs e)
        {

            userChampionInput = UserInpput.Text;
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
            errorMessage = 0;
            //The .json is case sensitive, some champion names have spaces and punctuation. Kha'Zix = "KhaZix" but also some like Rek'Sai = "Reksai" (usually the older 2014 champions), Miss Fortune - "MissFortune".

            if (PeskyChampions() == false)
            {

                if (ChampionInput.Contains(" "))
                {
                    string s = ChampionInput[0].ToString();
                    string s1 = ChampionInput[ChampionInput.Length - 1].ToString();
                   
                    //Removing the instances of " " and "'" is useful to call the API, but meant initially the input accepted stuff like "Z     ed", so added some conditions to check
                    // for this kind of scenario
                    char ch = ' ';
                    int freq = ChampionInput.Split(ch).Length - 1;
    
                    int countCaps = 0;                   
                    for (int i = 0; i < ChampionInput.Length; i++)
                    {
                        if (char.IsUpper(ChampionInput[i])) countCaps++;
                    }

                    if (s == " " || s1 == " " || (freq >= 1 && countCaps == 1) || (freq > 1 && countCaps >= 2))
                    {
                        errorMessage = 2;
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
                   
                    string ch = "'";
                    int freq = ChampionInput.Split(ch.ToCharArray()).Length - 1;

                    if (s == "'" || s1 == "'" || freq > 1)
                    {
                        errorMessage = 3;
                        ErrorAdvice();
                    }
                    else
                    {
                        ChampionInput = ChampionInput.Replace("'", string.Empty);
                        
                        IsItValid();

                        if(IsItValid() == false)
                        {
                            ChampionInput = ChampionInput.ToLower();
                            ChampionInput = char.ToUpper(ChampionInput[0]) + ChampionInput.Substring(1);
                        }
                        ValidateAndGo();
                        
                    }

                }
                else
                {
                  ValidateAndGo();
                }
            }


        }
        public void ValidateAndGo()
        {               
            
                if (IsItValid() == false)
                {
                errorMessage = 1;
                ErrorAdvice();
                }
                else
                {
                    Window1 win2 = new Window1(ChampionInput);
               // win2.championName = ChampionInput;
                    win2.Show();
                }
            
        }

        public bool IsItValid()
        {
            //the easiest way to validate the input is to pass it through the GetRequest
            Window1 win2 = new Window1(ChampionInput);


            if (win2.GetRequest())
            {
                win2.Close();
                return true;
            }
            else
            {
                win2.Close();
                return false;
            }
        }

        public void ErrorAdvice()
        {
            
            ErrorWindow error = new ErrorWindow(errorMessage);
            error.Show();
            //search is mostly case insensitive but better to advise otherwise as this is not universal for all champs
            //MessageBox.Show("You must input a valid champion name. The search is case sensitive for two part names, make sure to include the correct punctuation and spaces.");
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
            //Special case champions which have no rhyme or reason to them, their in-game and known by name is not the same in the API
            //Wukong's ID is "monkeyking" in API
            //Nunu & Willump is Nunu
            //Renata Glasc is Renata

            if (ChampionInput.Length == 0)
            {
                errorMessage = 4;
                ErrorAdvice();
                return true;
            }
            else if (ChampionInput == "Wukong")
            {
                 ChampionInput = "MonkeyKing";
                 ValidateAndGo();
                 return true;
            }
            
            else if (ChampionInput == "Nunu & Willump" || ChampionInput == "Nunu")
            {
                userChampionInput = "Nunu & Willump";
                ChampionInput = "Nunu";
                ValidateAndGo();
                return true;
            }
            else if(ChampionInput == "Renata Glasc")
            {
                ChampionInput = "Renata";
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


