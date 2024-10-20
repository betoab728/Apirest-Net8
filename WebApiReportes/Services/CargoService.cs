
using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApiReportes.models;
using WebApiReportes.Repositories;


namespace WebApiReportes.Services
{
    public class CargoService: ICargoService     {
        private readonly ICargoRepository _cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<Cargo>> GetAllCargosAsync()
        {
            return await _cargoRepository.GetAllCargosAsync();
        }

        public async Task<Cargo> GetCargoByIdAsync(int id)
        {
            return await _cargoRepository.GetCargoByIdAsync(id);
        }

        public async Task AddCargoAsync(Cargo cargo)
        {
            await _cargoRepository.AddCargoAsync(cargo);
        }

        public async Task UpdateCargoAsync(Cargo cargo)
        {
            await _cargoRepository.UpdateCargoAsync(cargo);
        }

        public async Task DeleteCargoAsync(int id)
        {
            await _cargoRepository.DeleteCargoAsync(id);
        }
        public async Task GetReporteCargosAsync(LocalReport report, IEnumerable<Cargo> cargos)
        {
            try
            {
                // Verifica la ruta correcta al archivo RDLC
                var rdlcPath = "WebApiReportes.reports.ReporteCargos.rdlc";

                // Cargar el archivo RDLC (asegúrate de que el path o recurso embebido sea correcto)

                using var rdlcStream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(rdlcPath);

                if (rdlcStream == null)
                {
                    throw new FileNotFoundException($"No se encontró el archivo RDLC: {rdlcPath}");
                }

                //creo un datatable para llenar los cargos con los campos : id,nombre,created_at,updated_at

                DataTable dt = new DataTable("cargoso");
                dt.Columns.Add("id", typeof(Int32)); // Asegúrate de que los nombres coincidan
                dt.Columns.Add("nombre", typeof(string));

                foreach (var cargo in cargos)
                {
                    dt.Rows.Add(cargo.id, cargo.nombre);

                }

                report.LoadReportDefinition(rdlcStream);

                var existingDataSource = report.DataSources["DataSet1"];
                if (existingDataSource != null)
                {
                    // Reemplazar el contenido del DataSource existente
                    existingDataSource.Value = dt;
                }
                else
                {
                    Console.WriteLine("No se encontró el DataSource existente");
                    // Si no existe, agregar el nuevo DataSource
                    report.DataSources.Add(new ReportDataSource("DataSet1", dt));
                }

            }
            catch (LocalProcessingException ex)
            {

                // Captura detalles específicos del error
                Debug.WriteLine($"Error en el procesamiento local: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Error interno: {ex.InnerException.Message}");
                }
            }
           
            catch (Exception ex)
            {
                Debug.WriteLine($"Error general: {ex.Message}");
            }
         
            
        }
    }
    
}
