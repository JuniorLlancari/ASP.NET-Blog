using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Categoria { get; }
        IArticleRepository Articulo { get; }

        void Save();

    }
}
