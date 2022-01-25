using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string GameUrl = "http://localhost:5000/Game/";
        private int gameSelected;
        private List<Game> games;
        private Processes process;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                process = new Processes(GameUrl);
                games = await process.GetAllGames();
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
                Game game = games.FirstOrDefault(i => i.name == gameName);
                gameSelected = game.id;

                process = new Processes(GameUrl + game.id);
                Game g = await process.GetGame();
                List<Game> list = new() { g };
                InformationBinding.ItemsSource = list;
                SetEditorTextBoxToEmpty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                process = new Processes(GameUrl);
                Game game = new()
                {
                    name = nameText.Text,
                    description = descriptionText.Text,
                    grade = int.Parse(gradeNumber.Text),
                    image = imageText.Text
                };
                await process.AddGame(game);
                SetEditorTextBoxToEmpty();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                process = new Processes(GameUrl + gameSelected);

                Game game = games.FirstOrDefault(i => i.id == gameSelected);

                game.id = game.id;
                game.name = nameText.Text;
                game.description = descriptionText.Text;
                game.grade = int.Parse(gradeNumber.Text);
                game.image = imageText.Text;
                await process.UpdateGame(game);
                SetEditorTextBoxToEmpty();
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

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                process = new Processes(GameUrl + gameSelected);
                await process.DeleteGame();
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

        private void SetEditorTextBoxToEmpty()
        {
            nameText.Text = "";
            descriptionText.Text = "";
            gradeNumber.Text = "";
            imageText.Text = "";
        }
    }
}
