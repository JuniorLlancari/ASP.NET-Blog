using BlogCore.AccesoDatos.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models.ViewModels;
using System.IO;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _hostEnvironment;

        public ArticleController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
    
            return View();
        }



        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM articleVm = new ArticuloVM()
            {
                Articulo = new Models.Entities.Articulo(),
                Categorias = _unitOfWork.Categoria.GetListaCategorias()
            };
            return View(articleVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {

                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (articuloVM.Articulo.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"images\articles\");
                    var extension = Path.GetExtension(archivos[0].FileName);


                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articuloVM.Articulo.UrlImagen = @"\images\articles\" + nombreArchivo + extension;
                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _unitOfWork.Articulo.Add(articuloVM.Articulo);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));

                }
            }
            articuloVM.Categorias = _unitOfWork.Categoria.GetListaCategorias();
            return View(articuloVM);


         }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM articleVm = new ArticuloVM()
            {
                Articulo = new Models.Entities.Articulo(),
                Categorias = _unitOfWork.Categoria.GetListaCategorias()
            };


            if (id != null)
            {
                articleVm.Articulo = _unitOfWork.Articulo.Get(id.GetValueOrDefault());
            }
            return View(articleVm);
        }


        [HttpPost]
        public IActionResult Edit(ArticuloVM articuloVM)
        {
          if (ModelState.IsValid)
          {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;


                var articuloDB = _unitOfWork.Articulo.Get(articuloVM.Articulo.Id);

                if (archivos.Count()>0)
                {
                    //Editar
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"images\articles");
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);


                    var rutaImage = Path.Combine(rutaPrincipal, articuloDB.UrlImagen.TrimStart('\\'));
                    var rutaImage2 = Path.Combine(rutaPrincipal, articuloDB.UrlImagen);


                    if (System.IO.File.Exists(rutaImage))
                    {
                        System.IO.File.Delete(rutaImage);

                    }

                    //subimos nuevamente
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articuloVM.Articulo.UrlImagen = @"\images\articles\" + nombreArchivo + nuevaExtension;
                    articuloVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _unitOfWork.Articulo.Update(articuloVM.Articulo);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    //se conserva la img
                    articuloVM.Articulo.UrlImagen = articuloDB.UrlImagen;
                }
                _unitOfWork.Articulo.Update(articuloVM.Articulo);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View();

            
        }


         [HttpDelete]
        public IActionResult Delete(int id)
        {
            var artDB=_unitOfWork.Articulo.Get(id);
            string rutaDirectorio = _hostEnvironment.WebRootPath;
            var rutaImage = Path.Combine(rutaDirectorio, artDB.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImage))
            {
                System.IO.File.Delete(rutaImage);
            }
            if (artDB == null)
            {
                return Json(new
                {
                    success = false,
                    message="Error borrrando articulo"
                }) ;
            }
            _unitOfWork.Articulo.Remove(artDB);
            _unitOfWork.Save();

            return Json(new
            {
                success = true,
                message = "Borrrando articulo con exito"
            });


        }

        #region LLAMADAS A API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Articulo.GetAll(includeProperties: "Categoria") });

        }
 
        #endregion
    }
}
