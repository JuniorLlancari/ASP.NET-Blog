using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models.Entities
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
    
        [Required(ErrorMessage="El nombre es obligatorio")]
        [Display(Name ="Nombr articulo")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El descripcion es obligatorio")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha creación ")]
         public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
         public string UrlImagen { get; set; }
        

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
         



    }
}
