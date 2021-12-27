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

        public async Task<List<Game>> GetAllGames()
        {
            try
            {
                using (HttpClient client = new())
                {
                    return await client.GetFromJsonAsync<List<Game>>(Url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public async Task<Game> GetGame()
        {
            try
            {
                using (HttpClient client = new())
                {
                    return await client.GetFromJsonAsync<Game>(Url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
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
