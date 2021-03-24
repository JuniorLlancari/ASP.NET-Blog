using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Interfaces;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.Entities;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

         public readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();

             categoria = _unitOfWork.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            //valida lo del modelo ya esta conectado
            if (ModelState.IsValid)
            {
                _unitOfWork.Categoria.Update(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }


            return View(categoria);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            //valida lo del modelo ya esta conectado
            if (ModelState.IsValid)
            {
                _unitOfWork.Categoria.Add(categoria);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }   
            

            return View(categoria); 

        }


        #region LLAMADAS A API
        [HttpGet]
        public IActionResult GetAll()
        {
            var rpta= Json(new {data = _unitOfWork.Categoria.GetAll() });
           
            return rpta;
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoria = _unitOfWork.Categoria.Get(id);
            if (categoria == null)
            {
                return Json(new { success = false, message = "Error al eliminar" });
            }

            _unitOfWork.Categoria.Remove(categoria);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Categoria borrada" });

        }
        #endregion
    }
}
