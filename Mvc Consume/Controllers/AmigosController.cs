using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amigos_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc_Consume.Helper;
using Newtonsoft.Json;

namespace Mvc_Consume.Controllers
{
    public class AmigosController : Controller
    {
        AmigosApi _api = new AmigosApi();

        // GET: AmigosController
        public async Task<ActionResult> Index()
        {
            List<Amigo> amigos = new List<Amigo>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/amigo");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                amigos = JsonConvert.DeserializeObject<List<Amigo>>(result);
            }

            return View(amigos);
        }

        // GET: AmigosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var amigo = new Amigo();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/amigo/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                amigo = JsonConvert.DeserializeObject<Amigo>(result);
            }
            return View(amigo);
        }

        // GET: AmigosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AmigosController/Create

        [HttpPost]
        public async Task<IActionResult> Create(Amigo Amigo)
        {
            Amigo reciveAmigo = new Amigo();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Amigo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:51427/api/amigo", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    reciveAmigo = JsonConvert.DeserializeObject<Amigo>(apiResponse);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AmigosController/Edit/5
        // GET: PaisesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AmigosController/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Amigo amigo)
        {
            Amigo reciveAmigo = new Amigo();

            if (id != amigo.AmigoId)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(amigo), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:51427/api/amigo/"+$"{id}", content))
                {
                    string apiResponse = await
                    response.Content.ReadAsStringAsync();
                    reciveAmigo = JsonConvert.DeserializeObject<Amigo>(apiResponse);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AmigosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var amigo = new Amigo();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/amigo/{id}");
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }

        // POST: AmigosController/Delete/5
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
    }
}
