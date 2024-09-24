using GalacticTourAgency.ModelBindings;
using GalacticTourAgency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GalacticTourAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalacticProductsController : ControllerBase
    {
        private static List<GalacticProduct> _products = new List<GalacticProduct>
        {
            new GalacticProduct{Id=1,GalactincRating=5,ManifacturingDate=DateTime.Now.AddYears(-1),Name="Product 1",Planet="Merkür",Price = 4}
        };

        [HttpGet("{id}")]
        public ActionResult<GalacticProduct> Get(int id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);   
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        

        [HttpPost]
        public ActionResult<GalacticProduct> Post([FromBody] GalacticProduct product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = _products.Max(x => x.Id) + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id},product);
        }

        [HttpGet("products-at-location/{location}")]
        public ActionResult<IEnumerable<GalacticProduct>> GetProductsAtLocation([ModelBinder(BinderType =typeof(GalacticCoordinateBinder))]GalacticCordinate location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_products);

        }

    }
}
