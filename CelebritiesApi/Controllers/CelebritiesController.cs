using CelebritiesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CelebritiesApi.Controllers
{
    //ApiController ekleyerek api tanımını yaptık
    [ApiController]
    [Route("api/[controller]")] //route nı verdik
    public class CelebritiesController : ControllerBase
    {
        public static readonly List<Celebrity> celebrities = new List<Celebrity>()
        {
            new Celebrity{Id=1,Name="Tarkan",Profession="Pop Müzik Sanatçısı"},
            new Celebrity{Id=2,Name="Sıla",Profession="Pop Müzik Sanatçısı"},
            new Celebrity{Id=3,Name="Kenan İmirzalıoğlu",Profession="Oyuncu"},
            new Celebrity{Id=4,Name="Bergüzar Korel",Profession="Oyuncu"},
        };

        //Get = api/celebrities
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(celebrities);
        }
        //Get = api/celebrities/5
        //id ile get oldugunu belirttik
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var celebrity = celebrities.FirstOrDefault(x => x.Id == id);
            if(celebrity is null)
            {
                return NotFound();
            }
            return Ok(celebrity); //id yi geri döndürdük
        }

        //Post api/celebrities
        //post olabilmesi için verileri FromBodyden alıcak Celebirty tipinde
        [HttpPost]
        public IActionResult Post([FromBody] Celebrity celebirity)
        {
            celebirity.Id = celebrities.Max(x => x.Id) + 1;
            celebrities.Add(celebirity);
            //get methoduna gönderiyorum celebirtiyin id si ile birlikte
            return CreatedAtAction(nameof(Get),new { id = celebirity.Id }, celebirity);
        }
        //Put : api/celebrities/5
        //güncellenecek değerin Id sini parametre olarak gönderiyoruz ce celebirty değerlerini frombody den alıyoruz
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Celebrity updateCelebrity)
        {
            //gelen id yi buldum
            var celebrity = celebrities.FirstOrDefault(x => x.Id == id);
            if(celebrity is null)
            {
                return NotFound();
            }
            //verilerimi güncelledim
            celebrity.Name = updateCelebrity.Name;
            celebrity.Profession = updateCelebrity.Profession;
            return NoContent();
        }
        //Delete: api/celebrities/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var celebirty = celebrities.FirstOrDefault(x => x.Id == id);
            if (celebirty is null)
            {
                return NotFound();
            }
            celebrities.Remove(celebirty);
            return NoContent();
        }
    }
}
