using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuntosApi.Services;

namespace PointsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly PointsService _pointService;

        public PointsController(PointsService puntoService)
        {
            _pointService = puntoService;
        }

        [HttpGet]
        public ActionResult<List<Punto>> Get() =>
            _pointService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPunto")]
        public ActionResult<Punto> Get(string id)
        {
            var punto = _pointService.Get(id);

            if (punto == null)
            {
                return NotFound();
            }

            return punto;
        }

        [HttpPost]
        public ActionResult<Punto> Create(Punto punto)
        {
            _pointService.Create(punto);

            return CreatedAtRoute("GetPunto", new { id = punto.Id.ToString() }, punto);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Punto puntoIn)
        {
            var punto = _pointService.Get(id);

            if (punto == null)
            {
                return NotFound();
            }

            _pointService.Update(id, puntoIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var punto = _pointService.Get(id);

            if (punto == null)
            {
                return NotFound();
            }

            _pointService.Remove(punto.Id);

            return NoContent();
        }
    }
}