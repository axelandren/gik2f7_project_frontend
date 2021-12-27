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
        private readonly IGameRepository gr;
        private int gameSelected;
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
            gameSelected = game.id;

            Sl = new ServiceLayer(GameUrl + game.id);
            IEnumerable<Game> list = Sl.GetGame();
            InformationBinding.ItemsSource = list;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Sl = new ServiceLayer(GameUrl);
            Game game = new()
            {
                name = nameText.Text,
                description = descriptionText.Text,
                grade = int.Parse(gradeNumber.Text),
                image = imageText.Text
            };
            Sl.AddGame(game);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) // Updates adds a new object currently
        {
            Sl = new ServiceLayer(GameUrl + gameSelected);

            Game game = games.Where(i => i.id == gameSelected).FirstOrDefault();

            game.id = game.id;
            game.name = nameText.Text;
            game.description = descriptionText.Text;
            game.grade = int.Parse(gradeNumber.Text);
            game.image = imageText.Text;
            Sl.UpdateGame(game);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Game game = games.Where(i => i.id == gameSelected).FirstOrDefault();
            nameText.Text = game.name;
            descriptionText.Text = game.description;
            gradeNumber.Text = game.grade.ToString();
            imageText.Text = game.image;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Sl = new ServiceLayer(GameUrl + gameSelected);
            Sl.DeleteGame();
        }

        // Makes sure that textbox is number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
