using GalacticTourAgency.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GalacticTourAgency.Models
{
    public class GalacticProduct
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Ürün Adı Gereklidir")]
        [StringLength(100,MinimumLength =3,ErrorMessage ="Ürün adı 3 ile 100 karakter arasında olamlıdır")]
        public string Name { get; set; }
        [Range(0.1,1000,ErrorMessage ="Fiyat 0.1 ile 1000 arasında olmalıdır")]
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Gezegen Adı Boş geçilemez")]
        [RegularExpression("^(Merkür|Venüs|Mars)$",ErrorMessage ="Geçerli Bir Gezegene ait ürün değil")] //Merkür?
        public string Planet { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Üretim Tarihi")]
        public DateTime ManifacturingDate { get; set; }
        [Range(1,10)]
        [Display(Name="Reyting")]
        public int GalactincRating { get; set; }
        [GalacticElementComposition(minElements:2, maxElements:5)]
        public string Composition { get; set; }

        public GalacticCordinate Coordinate { get; set; }
    }
}
