using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

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
                using (WebClient wc = new())
                {
                    string json = wc.DownloadString(FetchUrl);
                    games = JsonConvert.DeserializeObject<List<Game>>(json);
                    return games;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return games;
            }
        }
    }
}
