using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HospitalApp.Helper
{
    public class DonorAPI
    {
        public HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://gouthamvishwam-eval-prod.apigee.net/blooddonorapiproxy/");
            return client;
        }
    }
}
