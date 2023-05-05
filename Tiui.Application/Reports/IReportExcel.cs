namespace Tiui.Application.Reports
{
    /// <summary>
    /// Abstracción para la generación de reportes
    /// </summary>
    public interface IReportExcel
    {
        byte[] ToExcel();
    }
}
