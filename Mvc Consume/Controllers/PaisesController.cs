using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc_Consume.Helper;
using Newtonsoft.Json;
using Paises_Api.Models;

namespace Mvc_Consume.Controllers
{
    public class PaisesController : Controller
    {
        PaisesApi _api = new PaisesApi();

        // GET: PaisesController
        public async Task<ActionResult> Index()
        {
            List<Pais> amigos = new List<Pais>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/pais");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                amigos = JsonConvert.DeserializeObject<List<Pais>>(result);
            }

            return View(amigos);
        }

        // GET: PaisesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pais = new Pais();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/pais/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                pais = JsonConvert.DeserializeObject<Pais>(result);
            }
            return View(pais);
        }

        // GET: PaisesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisesController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Pais pais)
        {
            var recivePais = new Pais();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:51482/api/pais", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    recivePais = JsonConvert.DeserializeObject<Pais>(apiResponse);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: PaisesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaisesController/Edit/5
  
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Pais pais)
        {
            Pais recivePais = new Pais();

            if (id != pais.PaisId)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:51482/api/pais/" + $"{id}", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    recivePais = JsonConvert.DeserializeObject<Pais>(apiResponse);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PaisesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pais = new Pais();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/pais/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }

        // POST: PaisesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Foto(int id)
        {
            return View();
        }
    }
}
