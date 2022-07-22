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
    public class ChampionNames
    {
        
    }
    public partial class Window1 : Window
    {
        //Change the string name here to get the latest version, the API is updated manually so sometimes the newest version may not be available yet
        public string Version = "12.13.1";

        public string ChampionName;

        public string ImageUrl = "http://ddragon.leagueoflegends.com/cdn/img/champion/loading/";
        public string GuideUrl = "https://www.mobafire.com/league-of-legends/champion/";
        public string BaseUrl = "http://ddragon.leagueoflegends.com";

        public string fixString;
        public string result;

        public string incorrectLore;
        public string correctLore;

        public string TagsString;

        public string TitleString;

        public Window1()
        {            
            InitializeComponent();
            ProcessImage();
            GetRequest();
        }


        private void Link_Click(object sender, RoutedEventArgs e)
        {
            //Every link is specific to ChampionName input
            ChampionName = ((MainWindow)Application.Current.MainWindow).ChampionInput;
            Process.Start(new ProcessStartInfo(GuideUrl + ChampionName) { UseShellExecute = true });
        }


        public bool GetRequest()
        {
            //The get request for the json specific to ChampionName

            ChampionName = ((MainWindow)Application.Current.MainWindow).ChampionInput;
           
            string apiResponseAsJsonString = String.Empty;

            string url = "http://ddragon.leagueoflegends.com/cdn/"+Version+"/data/en_US/champion/" + ChampionName + ".json";

            var methodType = Method.Get;

            try
            {

                RestClient client = new RestClient(BaseUrl);
                RestRequest request = new RestRequest(url, methodType);
                RestResponse response = client.ExecuteGet(request);

                var statusCode = response.StatusCode.ToString();
                apiResponseAsJsonString = response.Content;

                //To use the same object for every champion ChampionName is set to just "champion"
                fixString = apiResponseAsJsonString;
                var result = fixString.Replace(ChampionName, "Champion");

                ChampionObject.Root jsonIntoObject = JsonConvert.DeserializeObject<ChampionObject.Root>(result);

                //Now setting all the variables
                //MonkeyKing case
                if(ChampionName == "MonkeyKing")
                {
                    ChampionName = "Wukong";
                }
                //Setting "Champion" back to the ChampionName
                incorrectLore = jsonIntoObject.data.champion.lore;
                correctLore = incorrectLore.Replace("Champion", ChampionName);

                //Some tags have 2 elements so try catch for this 
                TagsString = jsonIntoObject.data.champion.tags[0];
                try
                {
                    TagsString = jsonIntoObject.data.champion.tags[0] + " - " + jsonIntoObject.data.champion.tags[1];
                }
                catch (Exception ex)
                {

                }
               
                //The function adds a space between every char purely for aesthetics
                ChampionName = string.Join<char>(" ", ChampionName);
                Champion.Text = ChampionName.ToUpper();
          
                               
                TitleString = string.Join<char>(" ", jsonIntoObject.data.champion.title);
                Title.Text = TitleString.ToUpper();

                Lore.Text = correctLore;

                TagsString = string.Join<char>(" ", TagsString);
                Tags.Text = TagsString.ToUpper();

                AttackVal.Text = jsonIntoObject.data.champion.info.attack.ToString();
                DefenceVal.Text = jsonIntoObject.data.champion.info.defense.ToString();
                MagicVal.Text = jsonIntoObject.data.champion.info.magic.ToString();
                DifficultyVal.Text = jsonIntoObject.data.champion.info.difficulty.ToString();

                hitPointsVal.Text = jsonIntoObject.data.champion.stats.hp.ToString();
                moveSpeedVal.Text = jsonIntoObject.data.champion.stats.movespeed.ToString();
                manaPointsVal.Text = jsonIntoObject.data.champion.stats.mp.ToString();
                armorVal.Text = jsonIntoObject.data.champion.stats.armor.ToString();
                attackRangeVal.Text = jsonIntoObject.data.champion.stats.attackrange.ToString();
                attackSpeedVal.Text = jsonIntoObject.data.champion.stats.attackspeed.ToString();



                return true;
            }
            catch (Exception ex)
            {

                return false;

            }
        }
        public void ProcessImage()
        {
            //Every image is specific to ChampionName
            ChampionName = ((MainWindow)Application.Current.MainWindow).ChampionInput;
            Image imgPhoto = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + ChampionName + "_0.jpg");
            bitmap.EndInit();
            ChampionImage2.Source = bitmap;
        }
    }
}

