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

namespace LeagueApp
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public int errorNum;
        public string messageOne = "You must input a valid champion name. The search is case sensitive, make sure to include the correct punctuation and spaces.";
        public string messageTwo = "It looks like you've added a space somewhere you shouldn't have! Please try again.";
        public string messageThree = "It looks like you've added some punctuation where you shouldn't have! Please try again.";
        public string messageFour = "Please enter a champion!";
        public string poroString = "https://static.wikia.nocookie.net/leagueoflegends/images/d/d6/Sakura_Poro_profileicon.png/revision/latest?cb=20170505013242";
        public ErrorWindow(int errorMessage)
        {
            errorNum = errorMessage;
            InitializeComponent();
            ProcessImage();
            ProcessMessage();
        }
        public void ProcessImage()
        {
            Image imgPhoto = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(poroString);
            bitmap.EndInit();
            poro.Source = bitmap;
        }
        public void ProcessMessage()
        {
            int i = errorNum;
            if(errorNum == 1)
            {
                ErrorMessage.Text = messageOne;

            }
            if(errorNum == 2)
            {
                ErrorMessage.Text = messageTwo;
            }
            if(errorNum == 3)
            {
                ErrorMessage.Text = messageThree;
            }
            if(errorNum == 4)
            {
                ErrorMessage.Text = messageFour;
            }

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
