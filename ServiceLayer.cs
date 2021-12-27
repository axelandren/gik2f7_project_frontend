using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektWPF
{
    public class ServiceLayer
    {
        private string Url;
        public ServiceLayer(string Url)
        {
            this.Url = Url;
        }

        public List<Game> GetAllGames()
        {
            List<Game> games = new();
            try
            {
                using (WebClient client = new())
                {
                    string json = client.DownloadString(Url);
                    games = JsonSerializer.Deserialize<List<Game>>(json);
                    return games;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return games;
            }
        }

        public IEnumerable<Game> GetGame()
        {
            Game game = new();
            try
            {
                using (WebClient client = new())
                {
                    string json = client.DownloadString(Url);
                    game = JsonSerializer.Deserialize<Game>(json);
                    // Databinding requires IEnumerable object(list)
                    List<Game> list = new() { game };
                    return list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                List<Game> list = new() { game };
                return list;
            }
        }

        public async void AddGame(Game game)
        {
            try
            {
                using (HttpClient client = new())
                {
                    await client.PostAsJsonAsync(Url, game);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void UpdateGame(Game game)
        {
            try
            {
                using (HttpClient client = new())
                {
                    await client.PutAsJsonAsync(Url, game);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void DeleteGame()
        {
            try
            {
                using (HttpClient client = new())
                {
                    await client.DeleteAsync(Url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
