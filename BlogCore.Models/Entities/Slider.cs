using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models.Entities
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage ="Este datos es requerido!!")]
        public string Nombre { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Estado del slider")]
        public bool Estado { get; set; }


        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }
    }
}
