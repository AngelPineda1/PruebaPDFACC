using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System;

class Program
{
    static void Main(string[] args)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(12));

                page.Header()
                    .Padding(10)
                    .Row(row =>
                    {
                        row.RelativeColumn(1)
                            .AlignRight()
                            .Column(column =>
                            {
                                column.Item().Text("Reporte de Servicio").FontSize(20).Bold().FontColor(Colors.Blue.Darken2);
                                column.Item().Text($"Fecha: 2024/08/19  Folio: 11282").FontSize(10);
                            });
                    });

                page.Content()
                    .PaddingVertical(10)
                    .Column(column =>
                    {
                        column.Spacing(20); // Aumenta el espacio entre secciones

                        // Contenedor para información y firmas, sin borde exterior
                        column.Item()
                            .Padding(10)
                            .ExtendVertical() // Extiende para ocupar todo el espacio posible
                            .Column(contentColumn =>
                            {
                                contentColumn.Spacing(20); // Aumenta el espacio entre secciones internas

                                // Sección de Datos del Cliente y Usuario
                                contentColumn.Item()
                                    .Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1);
                                            columns.RelativeColumn(1);
                                        });

                                        // Fila de Headers
                                        table.Cell().Element(HeaderCellStyle).Text("Datos Cliente").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                                        table.Cell().Element(HeaderCellStyle).Text("Usuario").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);

                                        // Fila de Datos
                                        table.Cell().Element(CellStyle).Text("GEMTRON").FontSize(12);
                                        table.Cell().Element(CellStyle).Text("Alberto Alanis").FontSize(12);

                                        // Fila de Headers para "Servicio" y "OSE"
                                        table.Cell().Element(HeaderCellStyle).Text("Servicio").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                                        table.Cell().Element(HeaderCellStyle).Text("OSE").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);

                                        // Fila de Datos para "CAL Y RECOLECCION" y su información para "OSE"
                                        table.Cell().Element(CellStyle).Text("CAL Y RECOLECCION").FontSize(12);
                                        table.Cell().Element(CellStyle).Text("761").FontSize(12);

                                        // Fila de las horas en la columna izquierda
                                        table.Cell()
                                            .Element(CellStyle)
                                            .Column(column =>
                                            {
                                                column.Spacing(10); // Aumenta el espacio entre las filas de horas

                                                column.Item()
                                                    .Row(subRow =>
                                                    {
                                                        subRow.RelativeColumn().Text("Hora de entrada").Bold();
                                                        subRow.RelativeColumn().Text("9:50");
                                                    });

                                                column.Item()
                                                    .Row(subRow =>
                                                    {
                                                        subRow.RelativeColumn().Text("Hora de salida").Bold();
                                                        subRow.RelativeColumn().Text("12:20");
                                                    });

                                                column.Item()
                                                    .Row(subRow =>
                                                    {
                                                        subRow.RelativeColumn().Text("Hora de llegada a la planta").Bold();
                                                        subRow.RelativeColumn().Text("10:30");
                                                    });
                                            });

                                        // Columna para "Calibraciones Realizadas" con línea divisoria negra
                                        table.Cell()
                                            .Column(column =>
                                            {
                                                column.Item().Text("Calibraciones Realizadas").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                                                column.Item().PaddingBottom(10).LineHorizontal(1).LineColor(Colors.Black); // Aumenta el espacio debajo de la línea divisoria negra
                                                column.Item().Text("Partidas 1-2-3-4-5").FontSize(12);
                                            });

                                        // Fila de Headers para "Observaciones" que abarque ambas columnas
                                        table.Cell().ColumnSpan(2).Element(HeaderCellStyle).Text("Observaciones").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);

                                        // Fila de Datos para "Observaciones" que abarque ambas columnas
                                        table.Cell().ColumnSpan(2).Element(CellStyle).Text("Se hace recolección de vernier QAV-56 (Nvo). para calibración en el laboratorio. Este texto puede extenderse a varias líneas si es necesario.").FontSize(12);

                                        // Fila de Headers para "NSS" y "Nombre Ings. Calibración"
                                        table.Cell().Element(HeaderCellStyle).Text("NSS").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                                        table.Cell().Element(HeaderCellStyle).Text("Nombre Ings. Calibración").FontSize(14).Bold().FontColor(Colors.Blue.Darken2);

                                        // Fila de Datos para "NSS" y "Nombre Ings. Calibración"
                                        table.Cell().Element(CellStyle).Text("04139850962").FontSize(12);
                                        table.Cell().Element(CellStyle).Text("Alan Herrera").FontSize(12);
                                    });

                                // Sección de Firmas
                                contentColumn.Item()
                                   
                                    .Row(row =>
                                    {
                                        row.RelativeColumn()
                                            .Column(column =>
                                            {
                                                column.Item().Text("Nombre y Firma Usuario").FontSize(10).Bold().FontColor(Colors.Blue.Darken2);
                                                column.Item().PaddingTop(10).LineHorizontal(1); // Aumenta el espacio superior para la línea de firma
                                            });

                                        row.RelativeColumn()
                                            .Column(column =>
                                            {
                                                column.Item().Text("Nombre y Firma Ing. Calibración").FontSize(10).Bold().FontColor(Colors.Blue.Darken2);
                                                column.Item().PaddingTop(10).LineHorizontal(1); // Aumenta el espacio superior para la línea de firma
                                            });
                                    });

                                // Espacio para las imágenes debajo de las firmas
                                contentColumn.Item().PaddingTop(20).AlignCenter().Column(imagesColumn =>
                                {
                                    imagesColumn.Spacing(20); // Aumenta el espacio entre imágenes

                                    // Añadir la primera imagen
                                    // imagesColumn.Item().Image("path/to/image1.png", ImageScaling.FitWidth); // Ajusta la ruta de la imagen

                                    // Añadir la segunda imagen si es necesario
                                    // imagesColumn.Item().Image("path/to/image2.png", ImageScaling.FitWidth); // Ajusta la ruta de la imagen
                                });
                            });
                    });

                // Definimos el pie de página que aparecerá al fondo de cada página
                page.Footer()
                    .AlignRight()
                    .PaddingVertical(5)
                    .Text("F-7.8-02 Rev.03")
                    .FontSize(10)
                    .Italic()
                    .FontColor(Colors.Grey.Darken2);
            });
        });

        // Generar el PDF y mostrarlo en el previewer
        document.GeneratePdf("output.pdf");
        document.ShowInPreviewer();
    }

    // Método para estilizar las celdas de los headers con una línea divisoria
    static IContainer HeaderCellStyle(IContainer container)
    {
        return container
            .Padding(5)
            .BorderBottom(1) // Línea divisoria debajo de los headers
            .BorderColor(Colors.Grey.Darken2)
            .Background(Colors.White);
    }

    // Método para crear solo una línea divisoria negra
    static IContainer SimulatedLineHeaderCell(IContainer container)
    {
        return container
            .Padding(5)
            .BorderBottom(1) // Línea divisoria negra
            .BorderColor(Colors.Black) // Línea de color negro
            .ExtendHorizontal()
            .Background(Colors.White);
    }

    // Método para estilizar las celdas de la tabla
    static IContainer CellStyle(IContainer container)
    {
        return container
            .Padding(5)
            .BorderBottom(0) // Eliminamos la línea inferior
            .Background(Colors.White);
    }
}
