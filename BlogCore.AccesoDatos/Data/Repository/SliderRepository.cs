using BlogCore.AccesoDatos.Data.Interfaces;
using BlogCore.Data;
using BlogCore.Models.Entities;
using System.Linq;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {

        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Slider slider)
        {
            var sliderDB = _db.Slider.FirstOrDefault(x => x.Id == slider.Id);

            sliderDB.Nombre = slider.Nombre;
            sliderDB.UrlImagen = slider.UrlImagen;
            sliderDB.Estado = slider.Estado;
            _db.SaveChanges();


        }
    }
}
