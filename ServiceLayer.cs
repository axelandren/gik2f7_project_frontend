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
        private string FetchUrl;
        public ServiceLayer(string FetchUrl)
        {
            this.FetchUrl = FetchUrl;
        }

        public List<Game> GetAllGames()
        {
            List<Game> games = new();
            try
            {
                using (WebClient client = new())
                {
                    var json = client.DownloadString(FetchUrl);
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
                    string json = client.DownloadString(FetchUrl);
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
                    await client.PostAsJsonAsync(FetchUrl, game);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Game UpdateGame()
        {
            throw new NotImplementedException();
        }

        public Game DeleteGame()
        {
            throw new NotImplementedException();
        }
    }
}
