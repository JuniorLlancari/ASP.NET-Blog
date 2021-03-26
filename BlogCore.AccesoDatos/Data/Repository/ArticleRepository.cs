using BlogCore.AccesoDatos.Data.Interfaces;
using BlogCore.Data;
using BlogCore.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ArticleRepository:Repository<Articulo>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Articulo articulo)
        {
            var objArticulo = _db.Articulo.FirstOrDefault(a=>a.Id==articulo.Id);
            objArticulo.Nombre = articulo.Nombre;
            objArticulo.Descripcion = articulo.Descripcion;
            objArticulo.UrlImagen = articulo.UrlImagen;
            objArticulo.CategoriaId = articulo.CategoriaId;
            

        }
    }
}
