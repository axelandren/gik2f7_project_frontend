using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjektWPF
{
    public class ServiceLayer
    {
        private string FetchUrl;
        public ServiceLayer(string FetchUrl)
        {
            this.FetchUrl = FetchUrl;
        }
        public Response GetData()
        {
            Response data = null;
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(FetchUrl);
                data = JsonSerializer.Deserialize<Response>(json);
            }
            return data;
        }
    }
}
