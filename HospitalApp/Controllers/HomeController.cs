using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HospitalApp.Models;
using HospitalApp.Helper;
using System.Net.Http;
using Newtonsoft.Json;

namespace HospitalApp.Controllers
{
    public class HomeController : Controller
    {
        DonorAPI _api = new DonorAPI();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<DonorsData> donors = new List<DonorsData>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/donors");
            if(res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                donors = JsonConvert.DeserializeObject<List<DonorsData>>(results);
            }
            return View(donors);
        }

        public async Task<IActionResult> Details(int id)
        {
            DonorsData donors = new DonorsData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/donors/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                donors = JsonConvert.DeserializeObject<DonorsData>(results);
            }
            return View(donors);
        }

        [HttpPost]
        public IActionResult create(DonorsData donor)
        {
            HttpClient client = _api.Initial();

            var postTask = client.PostAsJsonAsync<DonorsData>("api/donors", donor);
            postTask.Wait();

            var result = postTask.Result;
            if(result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            DonorsData donors = new DonorsData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/donors/{id}");
          
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int id, DonorsData donor)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PutAsJsonAsync<DonorsData>($"api/donors/{id}",donor);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> EditAsync(int id)
        {
            DonorsData donors = new DonorsData();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/donors/{id}");
            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                donors = JsonConvert.DeserializeObject<DonorsData>(results);
            }
            return View(donors);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
