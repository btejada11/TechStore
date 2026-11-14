using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TechStore.Models;

namespace TechStore.Reports
{
    public class ProductoDocument : IDocument
    {
        private readonly IEnumerable<Producto> _productos;

        public ProductoDocument(IEnumerable<Producto> productos)
        {
            _productos = productos;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    // Pie de página simplificado y compatible
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Medium));
                        text.Span("Página ");
                        text.CurrentPageNumber();
                        text.Span(" de ");
                        text.TotalPages();
                    });
                });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("TechStore").Bold().FontSize(22).FontColor(Colors.Blue.Medium);
                    column.Item().Text("Reporte General de Inventario de Productos").FontSize(14).Bold().FontColor(Colors.Grey.Darken2);
                    column.Item().Text($"Fecha de emisión: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(9).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(15).Table(table =>
            {
                // Definir las 6 columnas solicitadas en la rúbrica
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3); // Nombre
                    columns.RelativeColumn(4); // Descripción
                    columns.RelativeColumn(2); // Precio
                    columns.RelativeColumn(2.5f); // Categoría
                    columns.RelativeColumn(1.5f); // Stock
                    columns.RelativeColumn(1.5f); // Estado
                });

                // Cabecera de la tabla
                table.Header(header =>
                {
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Nombre").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Descripción").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Precio").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Categoría").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Stock").FontColor(Colors.White).Bold();
                    header.Cell().Background(Colors.Blue.Medium).Padding(5).Text("Estado").FontColor(Colors.White).Bold();
                });

                // Filas con los datos de la Base de Datos
                foreach (var item in _productos)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Nombre ?? "").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Descripcion ?? "S/D").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text($"${item.Precio:N2}").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Categoria?.Nombre ?? "Sin categoría").FontSize(9);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.Stock.ToString()).FontSize(9);

                    string estadoTexto = item.Estado.ToString();
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(estadoTexto).FontSize(9);
                }
            });
        }
    }
}