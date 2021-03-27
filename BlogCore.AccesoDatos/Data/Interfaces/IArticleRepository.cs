using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.Entities;

namespace BlogCore.AccesoDatos.Data.Interfaces
{
    public interface IArticleRepository:IRepository<Articulo>
    {
         void Update(Articulo categoria);

    }
}
