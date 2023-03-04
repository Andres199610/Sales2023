﻿using System;
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


        [Display(Name = "Producto Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener mas {1} caractéres")]
        public String Name { get; set; } = null!;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
