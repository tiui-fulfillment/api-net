using Microsoft.Reporting.NETCore;
using System.Data;
using Tiui.Application.File;
using Tiui.Application.Reports;
using Tiui.Entities.Guias;
using Tiui.Reports.DataAccess;
using Tiui.Reports.Templates;

namespace Tiui.Reports.Reports
{
    /// <summary>
    /// Clase para el manejo del reporte de la guía
    /// </summary>
    public class GuiaReport : IGuiaReport
    {
        private readonly GuiaDataAccess _guiaDataAccess;
        private readonly IFileService _fileService;
        private readonly dsReportsModels.GuiaTemplateDataTable _template;

        public GuiaReport(GuiaDataAccess guiaDataAccess, IFileService fileService)
        {
            this._guiaDataAccess = guiaDataAccess;
            this._fileService = fileService;
            this._template = new dsReportsModels.GuiaTemplateDataTable();
        }
        /// <summary>
        /// Establece los parámetros iniciales para la generación del reporte
        /// </summary>
        /// <param name="guiaId">Identificador de la guía</param>
        /// <returns>Task de ejecución</returns>
        public async Task SetData(long guiaId)
        {
            this._template.Clear();
            Guia guia = await this._guiaDataAccess.Action(guiaId);
            var guiaRow = this._template.NewGuiaTemplateRow();
            guiaRow.GuiaId = guia.Folio;
            guiaRow.Producto = guia.NombreProducto;
            guiaRow.Medidas = guia.Paquete.GetMedidas();
            guiaRow.Peso = guia.Paquete.PesoCotizado.ToString();
            guiaRow.CobroContraEntrega = guia.EsPagoContraEntrega ? "Si" : "No";
            guiaRow.CantidadProductos = guia.CantidadPaquetes.ToString();

            guiaRow.NombreRemitente = guia.Remitente.Nombre;
            guiaRow.TelefonoRemitente = guia.Remitente.Telefono;

            guiaRow.NombreDestinatario = guia.Destinatario.Nombre;
            guiaRow.TelefonoDestinatario = guia.Destinatario.Telefono;
            guiaRow.DireccionDestinatario = guia.Destinatario.GetDireccion();
            guiaRow.Referencias = $"Entre calles: {guia.Destinatario.Cruzamiento}, {guia.Destinatario.Referencias}";
            guiaRow.CiudadOrigen = guia.Remitente.Estado;
            guiaRow.CiudadDestino = guia.Destinatario.Estado;
            guiaRow.ComisionContraEntrega = guia.ImporteContraEntrega;

      guiaRow.CodigoQR = guia.Folio;

      this._template.AddGuiaTemplateRow(guiaRow);
        }
        /// <summary>
        /// Obtiene el reporte de guia en formato PDF
        /// </summary>
        /// <returns>Array de byte con información del reporte</returns>
        public byte[] ToPdf()
        {
            var report = new LocalReport();
            report.DataSources.Add(new ReportDataSource("GuiaTemplate", this._template as DataTable));
            report.ReportPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"/Templates/GuiaReport.rdlc";            
            report.EnableHyperlinks = true;           
            report.EnableExternalImages = true;
            return report.Render("PDF");
        }
    }
}
