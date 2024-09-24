using FirstController.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FirstController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //api/products
        private static List<Product> _products = new List<Product>()
        {
            new Product { Id = 1,Name="Product1",Price=200 },
            new Product { Id = 2,Name="Product2",Price=300 },
        };
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            //httpget davranışın hepsini al
            return _products;
        }
        //burada id belirterek bu get methodunun bir id ye ihtiyaç duydugunu belirttik
        //("{id:int}") tipini belirleyebiliriz
        [HttpGet("{id:int:min(1)}", Name ="GetProductById")]
        public IActionResult GetAction(int id)
        {
            //parametreden gelen id ile ürünümü buldum
            var product = _products.FirstOrDefault(x => x.Id == id);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }



        //alpha alfabetik karakter
        //minumum 3 karakter olmasını belirledik
        //[HttpGet("search/{keyword:alpha:minlength(3)}")]
        ////search?keyword=deneme&page=1
        ////querystring için Fromquery ile alırız
        //public IActionResult Search(string keyword)
        //{

        //}

        //[HttpGet("search")]
        ////search?keyword=deneme&page=1
        ////querystring için Fromquery ile alırız
        //public IActionResult Search([FromQuery] string keyword, [FromQuery] int? page = 1)
        //{

        //}


        //[HttpGet("category/{categoryName}")]
        //public IActionResult GetProductsByCategory(string categoryName)
        //{

        //}

        //[HttpGet("date/{date:datetime}")]
        //public IActionResult GetProductsByDate(DateTime date)
        //{

        //}

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            //en büyük id buldum ve bir ekledim.şuan için static oldugu için mecbur böyle yapmam gerekiyor
            var id = _products.Max(x=> x.Id) + 1;
            //product ıd me ıd yi atadım
            product.Id = id;
            //ve products modelime parametre olarak gelen product ekledim
            _products.Add(product);
            //create de 201 döner
            //create kullansaydım yolu kendi yazmam gerekiyordu
            //createdataction kullanarak önce İşlemden sonra nereye gideceğini belirttim. 
            // 1-) Get methoduna gönderdim,parametre olarak id verdim.
            //birde product nesnesinin oluştugunu söyledim

            //bu kullanım ile oluşturlan kullanımın detaylarını gösteren bir url oluşturduk
            //return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
            return CreatedAtAction(nameof(Get), new { id = product.Id },product);
        }
        ////patch ile kısmen güncelleme
        //[HttpPatch("{id}")]
        //public IActionResult PartialUpdateProduct(int id, [FromBody] JsonPatchDocument<Product> patchDocument) 
        //{

        //}


        [HttpPut]
        public IActionResult Put(int id,[FromBody] Product product)
        {
            //product boş mu geldi dolu mu geldi
            if(product is null || id != product.Id)
            {
                return BadRequest(); //400 döner
            }
            //parametreden gelen id ile mevcut veride ki id karşılaştırdık
            var existingProduct = _products.FirstOrDefault(x=>x.Id == id);
            if(existingProduct is null)
            {
                return NotFound();
            }
            //verilerimi güncelleiyorum
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return Ok(existingProduct); //200 döner verinin halini gönderdim
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //eşleşen paraemetreyi aldık
            var existingProduct = _products.FirstOrDefault(x=>x.Id == id);
            if(existingProduct is null)
            {
                return NotFound();
            }
            //listeden eşleşen paremetreye ait veriyi sildik
            _products.Remove(existingProduct);
            //silme işleminden sonra işlem döndürmüyorum
            return NoContent();
        }
    }
}
