using Microsoft.AspNetCore.Mvc;
using WebApiReportes.Services;
using WebApiReportes.models;
using WebApiReportes.reports;
using QuestPDF.Fluent;
using Microsoft.Reporting.NETCore;
using System.IO;


namespace WebApiReportes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService;
       

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
         
        }

        // GET: api/Cargo
        [HttpGet]
        public async Task<IActionResult> GetAllCargos()
        {
            var cargos = await _cargoService.GetAllCargosAsync();
            return Ok(cargos);
        }

        // GET: api/Cargo/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoById(int id)
        {
            var cargo = await _cargoService.GetCargoByIdAsync(id);
            if (cargo == null)
                return NotFound();
            return Ok(cargo);
        }

        // POST: api/Cargo
        [HttpPost]
        public async Task<IActionResult> AddCargo([FromBody] Cargo cargo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _cargoService.AddCargoAsync(cargo);
            return CreatedAtAction(nameof(GetCargoById), new { id = cargo.id }, cargo);
        }

        // PUT: api/Cargo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCargo(int id, [FromBody] Cargo cargo)
        {
            if (id != cargo.id || !ModelState.IsValid)
                return BadRequest();

            await _cargoService.UpdateCargoAsync(cargo);
            return NoContent();
        }

        // DELETE: api/Cargo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCargo(int id)
        {
            await _cargoService.DeleteCargoAsync(id);
            return NoContent();
        }

        //endpoint para generar el reporte de cargos

        [HttpGet("reporte")]
        public async Task<IActionResult> GenerarReporteCargos(string format = "PDF", string extension = "pdf")
        {
          
            // Obtener los datos de cargos desde la base de datos
            var cargos = await _cargoService.GetAllCargosAsync();
            // Crear el reporte usando el servicio de reporte
            using var report = new LocalReport();
            // Usar ReportViewerCore.Report para cargar el reporte
            //_cargoService.getre(report, cargos);

            await _cargoService.GetReporteCargosAsync(report,cargos);

            // Renderizar el reporte como PDF
            var pdf = report.Render("PDF", null, out _, out _, out _, out _, out _);

            // Devolver el PDF como respuesta HTTP
            return File(pdf, "application/pdf", "CargosReport.pdf");
        }
    }
}
