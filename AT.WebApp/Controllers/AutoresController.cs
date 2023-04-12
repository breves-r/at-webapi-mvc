using AT.Entidade;
using AT.WebApp.Models;
using AT.WebApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AT.WebApp.Controllers
{
    [Authorize]
    public class AutoresController : Controller
    {
        // GET: AutoresController
        public ActionResult Index()
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7023/api/Autores").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Autor>>(jsonString);

            return View(result);
        }

        // GET: AutoresController/Details/5
        public ActionResult Details(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Autor>(jsonString);

            return View(result);
        }

        // GET: AutoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Autor model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                var json = JsonSerializer.Serialize<Autor>(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7023/api/Autores", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoresController/Edit/5
        public ActionResult Edit(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Autor>(jsonString);

            return View(result);
        }

        // POST: AutoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Autor model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Autor>(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7023/api/Autores/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api");


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AutoresController/Delete/5
        public ActionResult Delete(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Autores/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Autor>(jsonString);

            return View(result);
        }

        // POST: AutoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7023/api/Autores/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private HttpClient PrepareRequest()
        {
            var token = this.HttpContext.Session.GetString(UserAccount.SESSION_TOKEN_KEY);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            return httpClient;
        }
    }

}
