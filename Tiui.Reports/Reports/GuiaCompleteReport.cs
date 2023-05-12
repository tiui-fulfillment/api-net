using Microsoft.Reporting.NETCore;
using System;
using System.Data;
using System.IO;
using Tiui.Application.DTOs.Reports;
using Tiui.Application.Reports;
using Tiui.Reports.DataAccess;

namespace Tiui.Reports.Reports
{
    public class GuiaCompleteReport : IGuiaCompleteReport
    {
        private DataTable _template = new DataTable("GuiaCompleteTemplate");
        private readonly GuiaCompleteDataAccess _guiaCompleteDataAccess;

        public GuiaCompleteReport(GuiaCompleteDataAccess guiaCompleteDataAccess)
        {
            this._guiaCompleteDataAccess = guiaCompleteDataAccess;
        }

        public void SetData(GuiaReportFilterDTO filter)
        {
            this._template = this._guiaCompleteDataAccess.Action(filter);
        }

        public byte[] ToExcel()
        {
            var report = new LocalReport();
            report.DataSources.Add(new ReportDataSource("GuiasCompleteReport", this._template));
            report.ReportPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"/Templates/GuiasCompleteReport.rdlc";
            report.EnableHyperlinks = true;
            return report.Render("EXCELOPENXML");
        }
    }
}
