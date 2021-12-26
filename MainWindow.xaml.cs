using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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

namespace ProjektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string GameUrl = "http://localhost:5000/Game/";
        private List<Game> games;
        private ServiceLayer Sl;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Sl = new ServiceLayer(GameUrl);
            games = Sl.GetAllGames();
            AllGamesBinding.ItemsSource = games;
        }

        private void TextBlockMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock gameClicked = sender as TextBlock;
            string gameName = gameClicked.Text;
            Game game = games.Where(i => i.name == gameName).FirstOrDefault();

            Sl = new ServiceLayer(GameUrl + game.id);
            IEnumerable<Game> list = Sl.GetGame();
            InformationBinding.ItemsSource = list;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Sl = new ServiceLayer(GameUrl);
            Game g = new()
            {
                name = nameText.Text,
                description = descriptionText.Text,
                grade = int.Parse(gradeNumber.Text),
                image = imageText.Text
            };
            string postGame = JsonSerializer.Serialize(g);
            using (HttpClient client = new())
            {
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var sendGame = new StringContent(postGame.ToString(), Encoding.UTF8, "application/json");
                client.PostAsync(GameUrl, sendGame);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Makes sure that textbox is number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
