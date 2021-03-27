using BlogCore.AccesoDatos.Data.Interfaces;
using BlogCore.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class SliderController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _hostEnvironment;

        public SliderController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
           
            
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
 
                var ruta = Path.Combine(rutaPrincipal, @"images\sliders\");
                string nameFile = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(ruta, nameFile + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                slider.UrlImagen = @"\images\sliders\" + nameFile + extension;
                _unitOfWork.Slider.Add(slider);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();

        }


        #region LLAMADAS A LA API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Slider.GetAll() });
        }
        #endregion

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var objSlider = _unitOfWork.Slider.Get(id.GetValueOrDefault());

 

            return View(objSlider);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {


            if (ModelState.IsValid)
            {
                //Fijar que si enviaron archivos
                var archivos = HttpContext.Request.Form.Files;
                //Traer Objeto
                var sliderDesdeDb = _unitOfWork.Slider.Get(slider.Id);
                
                if (archivos.Count() > 0)
                {

                    string rutaPrincipal = _hostEnvironment.WebRootPath;
                    //Armamos  ruta para verificar su existencia
                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //Aquí subimos nuevamente el archivo
                    var extension = Path.GetExtension(archivos[0].FileName);
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var directorioArchivos = Path.Combine(rutaPrincipal, @"images\sliders");
                    using (var fileStreams = new FileStream(Path.Combine(directorioArchivos, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    //Guardando en nuestro objeto la nueva ruta
                    slider.UrlImagen = @"\images\sliders\" + nombreArchivo + extension;
                }
                else
                {
                    slider.UrlImagen = sliderDesdeDb.UrlImagen;
                }

                _unitOfWork.Slider.Update(slider);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));

            }

            return View();

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            var objSlider=_unitOfWork.Slider.Get(id.GetValueOrDefault());
            if (objSlider == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }


            string rutaPrincipal = _hostEnvironment.WebRootPath;
            var rutaImage = Path.Combine(rutaPrincipal, objSlider.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImage))
            {
                System.IO.File.Delete(rutaImage);
            }


            _unitOfWork.Slider.Remove(objSlider);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Slider borrado correctamente" });
        }


    }
}
