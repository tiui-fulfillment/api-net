namespace Tiui.Application.Reports
{
    /// <summary>
    /// Abstraccíón para la generación del reporte de la guía
    /// </summary>
    public interface IGuiaReport : IReportPdf
    {
        public Task SetData(long guiaId);
    }
}
