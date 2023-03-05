using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }


        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(255, ErrorMessage = "El campo {0} no puede tener mas {1} caractéres")]
        public String Name { get; set; } = null!;
        public ICollection<ProdCategory>? ProdCategories { get; set; }

        public int ProdCategoriesNumber => ProdCategories == null ? 0 : ProdCategories.Count;

    }
}
