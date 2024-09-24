using GalacticTourAgency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GalacticTourAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalacticToursController : ControllerBase
    {

        private static List<GalacticTour> _galacticTours = new List<GalacticTour>()
        {
            new GalacticTour{Id=1,PlanetName="Mars",Duration="2 ay",Price = 500000},
            new GalacticTour{Id=2,PlanetName="Venüs",Duration="1 ay",Price = 400000},
        };

        [HttpGet]
        public IEnumerable<GalacticTour> GetAll()
        {
            return _galacticTours;
        }

        //int tipinde en az 1 karakter gelicek dedik ve geçersiz ıd yi engelledik
        [HttpGet("{id:int:min(1)}")]
        public ActionResult<GalacticTour> GetTour(int id, [FromHeader(Name ="X-Planet")] string planet)
        {
            var tour = _galacticTours.FirstOrDefault(x => x.Id == id);
            if(tour == null)
            {
                return NotFound($"Tur Id {id} bulunamadı");
            }
            return Ok(tour);
        }
        //gezegen adına göre listeledik.alpha ile sadece harflerin girişine izin verdik
        [HttpGet("planet/{planetName:alpha}")]
        public ActionResult<IEnumerable<GalacticTour>> GetToursByPlanet(string planetName)
        {
            var planetTours = _galacticTours.Where(t=>t.PlanetName.Equals(planetName,StringComparison.OrdinalIgnoreCase));
            if (!planetTours.Any())
            {
                return NotFound($"{planetName} tour bulunamadı");
            }
            return Ok(planetTours);
        }

        //belli aralıktakı listeledik
        [HttpGet("price-range")]
        public ActionResult<IEnumerable<GalacticTour>> GetToursByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var filteredTours = _galacticTours.Where(t => t.Price >= minPrice && t.Price <= maxPrice);
            return Ok(filteredTours);
        }

        [HttpPost]
        public ActionResult<GalacticTour> Create([FromBody] GalacticTour tour)
        {
            var id = _galacticTours.Max(t => t.Id) + 1;
            tour.Id = id;
            _galacticTours.Add(tour);
            return CreatedAtAction(nameof(GetTour), new { id = tour.Id }, tour);
        }

        [HttpPost("create-package")]
        public ActionResult<GalacticTour> CreateTourPackage([FromBody] GalacticTourPackage tourPackage)
        {
            var tour = new GalacticTour()
            {
                Id = _galacticTours.Max(x => x.Id) + 1,
                PlanetName = tourPackage.Destination,
                Duration = $"{tourPackage.DurationInDays} Gün",
                Price = tourPackage.BasePrice * tourPackage.DurationInDays
            };
            _galacticTours.Add(tour);

            return CreatedAtAction(nameof(GetTour), new { id = tour.Id }, tour);
        }
        //Urlde birden fazla bilgi almak 
        [HttpPut("update/{id:int:min(1)}/{newPlanetName}")]
        public IActionResult UpdateTourPlanet(int id,string newPlanetName)
        {
            var tour = _galacticTours.FirstOrDefault(t => t.Id == id);
            if(tour is null)
            {
                return NotFound($"Tur Id {id} bulunamadı");
            }
            tour.PlanetName = newPlanetName;
            return NoContent();
        }
        //biri ıd ile siliyor biri tur adıyla
        [HttpDelete("{id:int:min(1)}")]
        [HttpDelete("cancel/{tourName}")]
        public IActionResult CancelTour(int? id,string tourName)
        {
            GalacticTour tourToRemove;
            //id iilem i geldi kontrol edeceğiz
            if (id.HasValue)
            {
                tourToRemove = _galacticTours.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                tourToRemove = _galacticTours.FirstOrDefault(t=>t.PlanetName.Equals(tourName, StringComparison.OrdinalIgnoreCase));
            }

            if(tourToRemove == null)
            {
                return NotFound("Belirtilen Tur Bulunamadı");
            }

            _galacticTours.Remove(tourToRemove);
            return NoContent();
        }

        [HttpPatch("reschedule/{id:int:min(1)}/{newDate:datetime}")]
        public IActionResult RescheduleTour(int id,DateTime newDate, [FromBody] JsonPatchDocument<GalacticTour> patchDocument)
        {
            var tour = _galacticTours.FirstOrDefault(x=>x.Id == id);
            if(tour is null)
            {
                return NotFound($"Tur id {id} bulunamadı");
            }

            tour.DepartureDate = newDate;
            patchDocument.ApplyTo(tour); //applyto ile değişikliği yansıttık

            return NoContent();
        }
        [HttpPost("complex-search")]
        public ActionResult<IEnumerable<GalacticTour>> ComplexSearch([FromQuery] string name, [FromQuery] decimal? minPrice, [FromHeader(Name = "X-Planet")] string planet, [FromBody] SearchCriteria searchCriteria)
        {
            //if(string.IsNullOrWhiteSpace(name))
            //    if(minPrice.HasValue)
            /*
             POST /api/galactictours/complex-search?name=test&minPrice=400
            Header: X-planet : Mars
            Body : {
                DepartureDate:"20223-01-01"
                Duration:"2 Ay"
            }
             */

            return Ok();
        }

    }
}
