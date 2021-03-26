using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Interfaces
{
    public interface IArticleRepository:IRepository<Articulo>
    {
         void Update(Articulo categoria);

    }
}
