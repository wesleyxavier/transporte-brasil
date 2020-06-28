using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using src.core.entities;
using src.core.services.veiculo;

namespace src.app.webapi.Controllers {
    [ApiController]
    [Route ("api/[controller]/[action]")]
    public class VeiculoController : ControllerBase {
        private readonly ILogger<VeiculoController> _logger;
        private readonly IVeiculoService _service;

        public VeiculoController (
            ILogger<VeiculoController> logger,
            IVeiculoService service) {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            var lista = await _service.FindAll ();
            return Ok (lista);
        }

        [HttpPost]
        public async Task<IActionResult> Add ([FromBody] Veiculo veiculo) {

            veiculo = await _service.Add (veiculo);

            if (veiculo == null) {
                return BadRequest ();
            }

            return Ok (veiculo);
        }

        [HttpPut]
        public async Task<IActionResult> Update ([FromBody] Veiculo veiculo) {

            veiculo = await _service.Update (veiculo);

            if (veiculo == null) {
                return BadRequest ();
            }

            return Ok (veiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Find ([FromQuery (Name = "id")] int id) {

            var veiculo = await _service.Find (id);

            if (veiculo == null) {
                return NoContent ();
            }

            return Ok (veiculo);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete ([FromQuery (Name = "id")] int id) {
            var item = await _service.Find (id);
            if (item == null) {
                return NotFound ();
            }

            await _service.Delete (item);

            return NoContent ();
        }
    }
}