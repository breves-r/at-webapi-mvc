using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;
using AT.Entidade;
using AT.WebApp.Models;
using AT.WebApp.Models.Account;

namespace AT.WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: UsuariosController
        public ActionResult Index()
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7023/api/Usuarios").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do usuário");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Usuario>>(jsonString);

            return View(result);
        }

        // GET: UsuariosController/Details/5
        public ActionResult Details(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do usuário");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // GET: UsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario model)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                var json = JsonSerializer.Serialize<Usuario>(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7023/api/Usuarios", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do usuário");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do usuário");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // POST: UsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Usuario model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Usuario>(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7023/api/Usuarios/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do usuário");


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Usuarios/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api do usuário");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Usuario>(jsonString);

            return View(result);
        }

        // POST: UsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7023/api/Usuarios/{id}").Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api do usuário");

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

