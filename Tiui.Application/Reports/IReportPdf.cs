namespace Tiui.Application.Reports
{
    /// <summary>
    /// Abstracción para la generación de reportes
    /// </summary>
    public interface IReportPdf
    {
        byte[] ToPdf();
    }
}
