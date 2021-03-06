using BlogCore.AccesoDatos.Data.Interfaces;
using BlogCore.Data;
using BlogCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db=db;
            Categoria = new CategoryRepository(_db);
            Articulo = new ArticleRepository(_db);
            Slider = new SliderRepository(_db);

        }

        public ICategoryRepository Categoria { get; private set; }

        public IArticleRepository Articulo { get; private set; }

        public ISliderRepository Slider { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
