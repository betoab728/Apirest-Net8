using Microsoft.Reporting.NETCore;
using System.Data;
using System.Reflection;
using WebApiReportes.models;

namespace WebApiReportes.Services
{
    public class CargoReportService
    {
        public static void Load(LocalReport report, IEnumerable<Cargo> cargos) 
        {
            // Crear la instancia del reporte
            // var report = new LocalReport();

            // Verifica la ruta correcta al archivo RDLC
            var rdlcPath = "WebApiReportes.reporte.Report1.rdlc";

            // Cargar el archivo RDLC (asegúrate de que el path o recurso embebido sea correcto)
        
            using var rdlcStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(rdlcPath);

            if (rdlcStream == null)
            {
                throw new FileNotFoundException($"No se encontró el archivo RDLC: {rdlcPath}");
            }

            //creo un datatable para llenar los cargos con los campos : id,nombre,created_at,updated_at

            DataTable dt = new DataTable("Cargo");
            dt.Columns.Add("id", typeof(string)); // Asegúrate de que los nombres coincidan
            dt.Columns.Add("nombre", typeof(string));
            dt.Columns.Add("created_at", typeof(string));
            dt.Columns.Add("updated_at", typeof(string));

            foreach (var cargo in cargos)
            {
                dt.Rows.Add(cargo.id, cargo.nombre, cargo.created_at, cargo.updated_at);
            }

            report.LoadReportDefinition(rdlcStream);

            //limpiar los datos del reporte
            report.DataSources.Clear();

            // Agregar los datos (asegúrate de usar el nombre que colocaste en el DataSource del RDLC)
            report.DataSources.Add(new ReportDataSource("DatasetCargo", dt));

           // return report;
        }

    }
}
