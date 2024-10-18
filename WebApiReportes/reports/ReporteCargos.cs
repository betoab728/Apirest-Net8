using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WebApiReportes.models;

namespace WebApiReportes.reports
{
    public class ReporteCargos : IDocument
    {
        private readonly IEnumerable<Cargo> _cargos; // Almacenar la lista de cargos

        // Constructor que recibe la lista de cargos
        public ReporteCargos(IEnumerable<Cargo> cargos)
        {
            _cargos = cargos;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30); // Establecer márgenes
                page.Header().Text("Reporte de Ejemplo").FontSize(20).Bold();
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Página ");
                    x.CurrentPageNumber();
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Text("Listado de Cargos").FontSize(16).Bold();
                // Recorrer la lista de cargos y mostrarlos en una tabla o lista
                // Crear una tabla para los cargos
                column.Item().Table(table =>
                {
                    // Definir el ancho de las columnas (2 columnas en este caso)
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1); // Columna para ID
                        columns.RelativeColumn(2); // Columna para Nombre
                    });

                    // Fila de encabezado
                    table.Header(header =>
                    {
                        //encabezado de la tabla
                        header.Cell().Text("ID").Bold();
                        header.Cell().Text("Nombre").Bold();
                    });

                    // Rellenar la tabla con los datos de cargos
                    foreach (var cargo in _cargos)
                    {
                        //agregar los datos de los cargos a la tabla
                        table.Cell().Element(CellStyle).Text(cargo.id.ToString()).FontSize(12);
                        table.Cell().Element(CellStyle).Text(cargo.nombre).FontSize(12);
                    }
                });
            });
        }

        // Método auxiliar para aplicar estilos a las celdas (bordes, padding, etc.)
        IContainer CellStyle(IContainer container)
        {
            return container
                .PaddingVertical(5)
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2);
        }


    }
}
