using Tiui.Application.DTOs.Reports;

namespace Tiui.Application.Reports
{
    public  interface IGuiaCompleteReport: IReportExcel
    {
        public void SetData(GuiaReportFilterDTO filter);
    }
}
