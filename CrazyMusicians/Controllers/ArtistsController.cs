using CrazyMusicians.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {

        static List<ArtistEntity> _artists = new List<ArtistEntity>()
        {
            new ArtistEntity{Id=1,Name="Ahmet Çalgı",Profession="Ünlü Çalgı Çalar",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=2,Name="Zeynep Melodi",Profession="Melodi Yazar",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=3,Name="Cemil Akor",Profession="Çılgın Akorist",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=4,Name="Fatma Nokta",Profession="Süpriz Nota üreticisi",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=5,Name="Hasan Ritim",Profession="Ritim Canavarı",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=6,Name="Elif Armoni",Profession="Armoni Ustası",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=7,Name="Ali Perde",Profession="Perde Uygulayıcı",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=8,Name="Ayşe Rezonans",Profession="Rezonans Uzmanı",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=9,Name="Murat Ton",Profession="Tonlama Meraklısı",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
            new ArtistEntity{Id=10,Name="Selin Akor",Profession="Akor Sihirbazı",FunFeature="Her zaman yanlış nota çalar,ama eğlenceli"},
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_artists);
        }

        [HttpGet("detail/{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            var artist = _artists.FirstOrDefault(x => x.Id == id);
            if(artist == null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        [HttpPost("add-artist")]
        public IActionResult AddArtist([FromBody] ArtistEntity entity)
        {
            var id = _artists.Max(x => x.Id) + 1;
            entity.Id = id;
            _artists.Add(entity);
            return CreatedAtAction(nameof(Get), new {id = entity.Id},entity);
        }

        [HttpPatch("change-status/{id:int:min(1)}")]
        public IActionResult ToogleArtist(int id)
        {
            var artist = _artists.FirstOrDefault(y => y.Id == id);
            if(artist is null)
            {
                return NotFound();
            }
            artist.Status = !artist.Status;
            return Ok(artist);

        }

        [HttpPut]
        public IActionResult UpdateArtist(int id, [FromBody] ArtistEntity entity)
        {
 
            if(entity is null || id != entity.Id)
            {
                return BadRequest();
            }
            var artist = _artists.FirstOrDefault(x => x.Id == id);
            if(artist is null)
            {
                return NotFound();
            }
            artist.Name = entity.Name;
            artist.Profession = entity.Profession;
            artist.FunFeature = entity.FunFeature;
            return Ok(artist);

        }

        [HttpDelete]
        public IActionResult DeleteArtist(int id)
        {
            var artist = _artists.FirstOrDefault(x=>x.Id == id);
            if (artist is null)
            {
                return NotFound();
            }
            artist.IsDeleted = true;
            return NoContent();
        }
        [HttpGet("search")]
        public IActionResult SearchArtist([FromQuery] string name, [FromQuery] string profession)
        {
            var filteredArtist = _artists.AsQueryable();
            if(!string.IsNullOrEmpty(name))
            {
                filteredArtist = filteredArtist.Where(a => a.Name.Contains(name,StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(profession))
            {
                filteredArtist = filteredArtist.Where(a => a.Profession.Contains(profession, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(filteredArtist.ToList());
        }
    }
}
