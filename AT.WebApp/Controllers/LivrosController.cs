using AT.Entidade;
using AT.WebApp.Models;
using AT.WebApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AT.WebApp.Controllers
{
    [Authorize]
    public class LivrosController : Controller
    {
        // GET: LivrosController
        public ActionResult Index()
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7023/api/Livros").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Livro>>(jsonString);

            return View(result);
        }

        // GET: LivrosController/Details/5
        public ActionResult Details(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Livros/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Livro>(jsonString);

            return View(result);
        }

        // GET: LivrosController/Create
        public ActionResult Create()
        {
            List<Autor> autores = GetAutores();
            ViewBag.Autores = new MultiSelectList(autores, nameof(Autor.Id), nameof(Autor.Nome));
            
            return View();
        }

        // POST: LivrosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Livro model, List<int> idAutores)
        {
            if (ModelState.IsValid == false)
                return View(model);

            try
            {
                var requestBody = new
                {
                    livro = model,
                    idAutores = idAutores
                };
                var json = JsonSerializer.Serialize(requestBody);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PostAsync($"https://localhost:7023/api/Livros", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivrosController/Edit/5
        public ActionResult Edit(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Livros/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Livro>(jsonString);

            return View(result);
        }

        // POST: LivrosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Livro model)
        {
            try
            {
                var json = JsonSerializer.Serialize<Livro>(model);
                StringContent content = new StringContent(json, new MediaTypeHeaderValue("application/json"));

                var httpClient = PrepareRequest();

                var response = httpClient.PutAsync($"https://localhost:7023/api/Livros/{id}", content).Result;

                if (response.IsSuccessStatusCode == false)
                    throw new Exception("Erro ao tentar chamar a api");


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LivrosController/Delete/5
        public ActionResult Delete(int id)
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync($"https://localhost:7023/api/Livros/{id}").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<Livro>(jsonString);

            return View(result);
        }

        // POST: LivrosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var httpClient = PrepareRequest();

                var response = httpClient.DeleteAsync($"https://localhost:7023/api/Livros/{id}").Result;

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

        private List<Autor> GetAutores()
        {
            var httpClient = PrepareRequest();

            var response = httpClient.GetAsync("https://localhost:7023/api/Autores").Result;

            if (response.IsSuccessStatusCode == false)
                throw new Exception("Erro ao tentar chamar a api");

            var jsonString = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<List<Autor>>(jsonString);

            return result;
        }
    }
}
