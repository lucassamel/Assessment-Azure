using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc_Consume.Helper;

namespace Mvc_Consume.Controllers
{
    public class EstadosController : Controller
    {
        PaisesApi _api = new PaisesApi();

        // GET: EstadosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EstadosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: EstadosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: EstadosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
    }
}
