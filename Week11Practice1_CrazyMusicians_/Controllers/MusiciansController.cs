using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Week11Practice1_Crazy_Musicians.Entities;

namespace Week11Practice1_Crazy_Musicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {

        static List<Musician> _musicians = new List<Musician>()
        {
            new Musician{Id = 1, FullName="Ahmet Çalgı", Job="Ünlü Çalgı Çalar", FunFeature="Her zaman yanlış nota çalar ama çok eğlenceli"},
            new Musician{Id = 2, FullName="Zeynep Melodi", Job="Popüler Melodi Yazarı", FunFeature="Şarkıları yanlış anlaşılır ama çok popüler"},
            new Musician{Id = 3, FullName="Cemil Okar", Job="Çılgın Akorist", FunFeature="Akorları sık değişir ama şaşırtısı derecede yetenekli"},
            new Musician{Id = 4, FullName="Fatma Nota", Job="Süpriz Nota Üreticisi", FunFeature="Nota Üretirken sürekli süprizler hazırlar"},
            new Musician{Id = 5, FullName="Hasan Ritim", Job="Ritim Canavarı", FunFeature="Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir"},

        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_musicians);
        }


        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            if (musician is null)
                return NotFound();
            return Ok(musician);
        }

        //locolhost:1717/api/ToDos/search?keyword=....
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {
            var musicians = _musicians.Where(x => x.Job.Equals(keyword, StringComparison.OrdinalIgnoreCase) || x.FunFeature.Equals(keyword, StringComparison.OrdinalIgnoreCase));

            if(musicians.Any())
                return NotFound();
            return Ok(musicians);
        }


        [HttpPost]
        public IActionResult Post([FromBody] Musician musician)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            musician.Id = _musicians.Max(x => x.Id) + 1;
            _musicians.Add(musician);

            return CreatedAtAction(nameof(Get), new { id = musician.Id }, musician);

        }


        [HttpPut("update/{id:int:min(1)}")]
        public IActionResult Put(int id, [FromBody] Musician request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (request is null || id != request.Id)
                return BadRequest();

            musician.FullName = request.FullName;
            musician.Job = request.Job;
            musician.FunFeature = request.FunFeature;

            return Ok(musician);
        }

        [HttpPatch("{id:int:min(1)}")]
        public IActionResult PatchMusician(int id,string newJob,
            [FromBody] JsonPatchDocument<Musician> patchDoc)
        {
       

            if (patchDoc is null)
                return BadRequest();
            

            // İlgili müzisyeni bul
            var musician = _musicians.FirstOrDefault(m => m.Id == id);

            if (musician is null)
            {
                return NotFound();
            }
            musician.Job = newJob;

            // Patch işlemi uygula
            patchDoc.ApplyTo(musician);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            return Ok(musician);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);
            
            if(musician is null) 
                return NotFound();

            musician.IsDeleted = true;
            return Ok(musician);


        }

    }
}
