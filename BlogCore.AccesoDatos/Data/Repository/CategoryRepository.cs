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
    public class CategoryRepository:Repository<Categoria>,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(x => new SelectListItem()
            {
                Text=x.Nombre,
                Value=x.Id.ToString()
            });
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(x => x.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;
            _db.SaveChanges();
        }
    }
}
