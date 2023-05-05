using Npgsql;
using System.Data;
using System.Text;
using Tiui.Application.DTOs.Reports;

namespace Tiui.Reports.DataAccess
{
    public class GuiaCompleteDataAccess
    {
        private readonly NpgsqlConnection _connection;

        public GuiaCompleteDataAccess(NpgsqlConnection connection)
        {
            this._connection = connection;
        }
        /// <summary>
        /// Obtiene información de la guía 
        /// </summary>
        /// <param name="guiaId">Identificador de la guia a buscar</param>
        /// <returns>Guia encontrada</returns>
        public DataTable Action(GuiaReportFilterDTO filterDTO)
        {            
            NpgsqlCommand command = this._connection.CreateCommand();
            command.CommandText = this.GetSqlQuery(filterDTO);
            this.setParameters(command, filterDTO);
            command.CommandType = CommandType.Text;
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable("GuiaCompleteTemplate");            
            adapter.Fill(table);
            return table;
        }
        private string GetSqlQuery(GuiaReportFilterDTO filterDTO)
        {
            StringBuilder sQuery = new StringBuilder();       
            sQuery.AppendLine(" select distinct g.\"FechaRegistro\", g.\"Folio\", g.\"NombreProducto\", g.\"CantidadPaquetes\", g.\"EsPagoContraEntrega\", g.\"ImporteContraEntrega\",  g.\"TieneSeguroMercancia\" ");
            sQuery.AppendLine(" , g.\"ImporteCalculoSeguro\" , e.\"Nombre\" as \"Estatus\" , bg2.\"FechaRegistro\" as FechaEstatus, g.\"FechaReagendado\" as FechaReagendado ");
            sQuery.AppendLine(" , p.\"Largo\", p.\"Alto\", p.\"Ancho\" , p.\"PesoCotizado\" ");
            sQuery.AppendLine(" , dd.\"Nombre\", dd.\"Empresa\", dd.\"Telefono\"  , dd.\"CorreoElectronico\" , dd.\"CodigoPostal\" , dd.\"Pais\" ,dd.\"Estado\" ,dd.\"Ciudad\", dd.\"Colonia\"  ");
            sQuery.AppendLine(" ,dd.\"Calle\" , dd.\"Cruzamiento\" , dd.\"Numero\" , dd.\"Referencias\"  ");
            sQuery.AppendLine(" , dg.\"Nombre\" as nombre_detinatario, dg.\"Empresa\" as empresa_destinatario, dg.\"Telefono\" as telefono_destinatario , dg.\"CorreoElectronico\" as email_destinatario ");
            sQuery.AppendLine(" , dg.\"CodigoPostal\" as codigo_destinatario , dg.\"Pais\" as pais_destinatario ,dg.\"Estado\" as estado_destinatario ,dg.\"Ciudad\" as ciudad_destinatario ");
            sQuery.AppendLine(" , dg.\"Colonia\" as colonia_destinatario ,dg.\"Calle\" as calle_destinatario , dg.\"Cruzamiento\" as cruzamiento_destinatario  ");
            sQuery.AppendLine(" , dg.\"Numero\" as numero_destinatario , dg.\"Referencias\" as referencias_destinatario ");
            sQuery.AppendLine(" , mc.\"Descripcion\" as Motivo_cancelacion ");
            sQuery.AppendLine(" from \"Guias\" g  ");
            sQuery.AppendLine(" inner join \"DireccionesGuia\" dd on g.\"GuiaId\" = dd.\"Remitente_GuiaId\" and dd.\"Discriminator\"  = 'Remitente' ");
            sQuery.AppendLine(" inner join \"DireccionesGuia\" dg on g.\"GuiaId\" = dg.\"GuiaId\" and dg.\"Discriminator\" = 'Destinatario' ");
            sQuery.AppendLine(" inner join \"Paquetes\" p on p.\"PaqueteId\" = g.\"PaqueteId\"  ");
            sQuery.AppendLine(" inner join(select max(\"FechaRegistro\") \"FechaRegistro\", \"GuiaId\"  from \"BitacoraGuias\" bg  ");
            sQuery.AppendLine(" group by \"GuiaId\") bg on bg.\"GuiaId\"  = g.\"GuiaId\"  ");
            sQuery.AppendLine(" inner join \"BitacoraGuias\" bg2 on bg2.\"FechaRegistro\"=bg.\"FechaRegistro\" and bg2.\"GuiaId\" = bg.\"GuiaId\"  ");
            sQuery.AppendLine(" inner join \"Estatus\" e on e.\"EstatusId\" = bg2.\"EstatusNuevo\" ");
            sQuery.AppendLine(" left join \"CancelacionGuias\" cg on cg.\"GuiaId\" = g.\"GuiaId\" ");
            sQuery.AppendLine("left join \"MotivosCancelaciones\" mc on mc.\"MotivoCancelacionId\" = cg.\"MotivoCancelacionId\"");
            return sQuery.ToString();
        }
        private void setParameters(NpgsqlCommand command, GuiaReportFilterDTO filterDTO)
        {
            StringBuilder sbWhere = new StringBuilder();
            if (filterDTO.FechaRegistroInicio.HasValue && filterDTO.FechaRegistroFin.HasValue)
            {                
                sbWhere.AppendLine("And g.\"FechaRegistro\" between @fechaInicial And @fechaFinal");
                command.Parameters.AddWithValue("@fechaInicial", filterDTO.FechaRegistroInicio.Value.Date);
                command.Parameters.AddWithValue("@fechaFinal", filterDTO.FechaRegistroFin.Value.Date.AddHours(23).AddMinutes(59));
            }
            if (filterDTO.tiuiAmigoId.HasValue)
            {
                sbWhere.AppendLine("And g.\"TiuiAmigoId\" = @tiuiAmigoId");
                command.Parameters.AddWithValue("@tiuiAmigoId", filterDTO.tiuiAmigoId);
            }
            if (filterDTO.EstatusId.HasValue)
            {
                sbWhere.AppendLine("And g.\"EstatusId\" = @EstatusId");
                command.Parameters.AddWithValue("@EstatusId", filterDTO.EstatusId);
            }

            if (sbWhere.Length > 0)
                command.CommandText += $"Where {sbWhere.ToString().Substring(4, sbWhere.Length - 4)}";
        }
    }
}
