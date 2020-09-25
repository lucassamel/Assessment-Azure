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
    public class EstadosController : Controller
    {
        PaisesApi _api = new PaisesApi();

        // GET: EstadosController
        public async Task<ActionResult> Index()
        {
            List<Estado> estados = new List<Estado>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/estado");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                estados = JsonConvert.DeserializeObject<List<Estado>>(result);
            }

            return View(estados);
        }

        // GET: EstadosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var estado = new Estado();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/pais/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                estado = JsonConvert.DeserializeObject<Estado>(result);
            }
            return View(estado);
        }

        // GET: EstadosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Estado estado)
        {
            var reciveEstaado = new Estado();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(estado), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:51482/api/estado", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    reciveEstaado = JsonConvert.DeserializeObject<Estado>(apiResponse);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EstadosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadosController/Edit/5
        
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Estado estado)
        {
            Estado reciveEstado = new Estado();

            if (id != estado.EstadoId)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(estado), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:51482/api/estado/" + $"{id}", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    reciveEstado = JsonConvert.DeserializeObject<Estado>(apiResponse);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: EstadosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var estado = new Estado();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/estado/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }

        // POST: EstadosController/Delete/5
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
