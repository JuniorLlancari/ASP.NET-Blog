using System;
using System.Collections.Generic;
using System.Text;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.Entities;

namespace BlogCore.AccesoDatos.Data.Interfaces
{
    public interface ISliderRepository:IRepository<Slider>
    {

        void Update(Slider slider);

    }
}
