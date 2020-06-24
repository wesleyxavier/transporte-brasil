using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transporte.Entidades;

namespace Transporte.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private static readonly Veiculo[] Summaries = new[]
        {
            new Veiculo("Freezing"), 
            new Veiculo("Bracing"), 
            new Veiculo("Chilly"), 
            new Veiculo("Cool"), 
            new Veiculo("Mild"), 
            new Veiculo("Warm"), 
            new Veiculo("Balmy"), 
            new Veiculo("Hot"), 
            new Veiculo("Sweltering"), 
            new Veiculo("Scorching")
        };

        private readonly ILogger<VeiculoController> _logger;

        public VeiculoController(ILogger<VeiculoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Veiculo> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Veiculo(Summaries[index].Nome)
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
