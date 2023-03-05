using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sales.Shared.Entities
{
    public class ProdCategory
    {
        public int Id { get; set; }


        [Display(Name = "Producto/Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener mas {1} caractéres")]
        public String Name { get; set; } = null!;

        public Category? Category { get; set; }

        public ICollection<Product>?Products { get; set; }

        public int ProductsNumber => Products == null ? 0 : Products.Count;
    }
}
