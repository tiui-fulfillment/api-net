using System.Collections.Generic;
using System.Linq;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;

namespace Tiui.Entities.State
{
  public static class GuiaRuleState
  {
    public static List<Estatus> GetStatus(List<Estatus> statusList, EEstatusGuia estatusGuia)
    {
      switch (estatusGuia)
      {
        case EEstatusGuia.DOCUMENTADO_CONSOLIDADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.CAMINO_CEDIS_CDMX
            ).ToList(); ;
        case EEstatusGuia.CAMINO_CEDIS_CDMX:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX
          ).ToList();
        case EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.EN_CEDIS_CDMX
          ).ToList();
        case EEstatusGuia.EN_CEDIS_CDMX:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.DOCUMENTADO_FULFILLMENT:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PREPARACION
          ).ToList();
        case EEstatusGuia.PREPARACION:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX
          ).ToList();
        case EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PRODUCTO_AGOTADO ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.PRODUCTO_AGOTADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.RUTA_ENTREGA:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA ||
            e.EstatusId == (int)EEstatusGuia.PRIMER_INTENTO_ENTREGA ||
            e.EstatusId == (int)EEstatusGuia.ENTREGAGO || 
            e.EstatusId == (int)EEstatusGuia.NO_VISITADO
          ).ToList();
        case EEstatusGuia.PREPARANDO_RUTA:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.REAGENDADO ||
            e.EstatusId == (int)EEstatusGuia.RUTA_ENTREGA
          ).ToList();
        case EEstatusGuia.PRIMER_INTENTO_ENTREGA:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.REAGENDADO
          ).ToList();
        case EEstatusGuia.NO_VISITADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA ||
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.REAGENDADO
          ).ToList();
        case EEstatusGuia.REAGENDADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.ENTREGAGO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.CONCILIADO
          ).ToList();
        case EEstatusGuia.CONCILIADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.PAGADO
          ).ToList();
        case EEstatusGuia.DOCUMENTADO_RECOLECCION:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.EN_CAMINO_RECOLECCION
          ).ToList();
        case EEstatusGuia.EN_CAMINO_RECOLECCION:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.ENVIO_RECOLECTADO
          ).ToList();
        case EEstatusGuia.ENVIO_RECOLECTADO:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION:
          return statusList.Where(e =>
            e.EstatusId == (int)EEstatusGuia.ENVIO_PARA_REVISION ||
            e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA
          ).ToList();
        case EEstatusGuia.ENVIO_PARA_REVISION:
            return statusList.Where(e =>
                e.EstatusId == (int)EEstatusGuia.PREPARANDO_RUTA ||
                e.EstatusId == (int)EEstatusGuia.REAGENDADO ||
                e.EstatusId == (int)EEstatusGuia.ENTREGAGO ||
                e.EstatusId == (int)EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION ||
                e.EstatusId == (int)EEstatusGuia.EN_CEDIS_CDMX ||
                e.EstatusId == (int)EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX ||
                e.EstatusId == (int)EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX ||
                e.EstatusId == (int)EEstatusGuia.PRODUCTO_AGOTADO
            ).ToList();
                default:
          return null;
      }
    }
  }
}
