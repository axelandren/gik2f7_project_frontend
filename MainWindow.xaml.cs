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
        private int gameSelected;
        private List<Game> games;
        private ServiceLayer Sl;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl = new ServiceLayer(GameUrl);
                games = await Sl.GetAllGames();
                AllGamesBinding.ItemsSource = games;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void TextBlockMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TextBlock gameClicked = sender as TextBlock;
                string gameName = gameClicked.Text;
                Game game = games.Where(i => i.name == gameName).FirstOrDefault();
                gameSelected = game.id;

                Sl = new ServiceLayer(GameUrl + game.id);
                Game g = await Sl.GetGame();
                List<Game> list = new() { g };
                InformationBinding.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl = new ServiceLayer(GameUrl + gameSelected);

                Game game = games.Where(i => i.id == gameSelected).FirstOrDefault();

                game.id = game.id;
                game.name = nameText.Text;
                game.description = descriptionText.Text;
                game.grade = int.Parse(gradeNumber.Text);
                game.image = imageText.Text;
                Sl.UpdateGame(game);
                nameText.Text = "";
                descriptionText.Text = "";
                gradeNumber.Text = "";
                imageText.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Game game = games.Where(i => i.id == gameSelected).FirstOrDefault();
                nameText.Text = game.name;
                descriptionText.Text = game.description;
                gradeNumber.Text = game.grade.ToString();
                imageText.Text = game.image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl = new ServiceLayer(GameUrl + gameSelected);
                Sl.DeleteGame();
                nameText.Text = "";
                descriptionText.Text = "";
                gradeNumber.Text = "";
                imageText.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Makes sure that textbox is number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
