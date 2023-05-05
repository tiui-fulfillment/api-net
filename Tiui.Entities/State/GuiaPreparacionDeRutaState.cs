using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
  /// <summary>
  /// Implementación del estatus GuiaPreparcionDeRutaState
  /// </summary>
  public class GuiaPreparcionDeRutaState : BaseState
  {
    public GuiaPreparcionDeRutaState(IBaseStateContext context)
    {
      if (context is Guia)
        this._context = context;
      else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
    }
    public override int? GetId()
    {
      return (int)EEstatusGuia.PREPARANDO_RUTA;
    }

    public override string GetName()
    {
      return "Preparando Ruta";
    }
    /// <summary>
    /// Maneja el cambio de estatus de la guía
    /// </summary>
    /// <exception cref="BusinessRuleException">Produce una exception si no se puede realizar el cambio de estatus</exception>
    public override void Handle()
    {
      Guia guia = (Guia)this._context;
      if (
            guia.EstatusId != (int)EEstatusGuia.EN_CEDIS_CDMX &&
            guia.EstatusId != (int)EEstatusGuia.DOCUMENTADO_CONSOLIDADO &&
            guia.EstatusId != (int)EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX &&
            guia.EstatusId != (int)EEstatusGuia.REAGENDADO &&
            guia.EstatusId != (int)EEstatusGuia.ENVIO_RECOLECTADO &&
            guia.EstatusId != (int)EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION &&
            guia.EstatusId != (int)EEstatusGuia.PRODUCTO_AGOTADO &&
            guia.EstatusId != (int)EEstatusGuia.RUTA_ENTREGA &&
            guia.EstatusId != (int)EEstatusGuia.ENVIO_PARA_REVISION &&
            guia.EstatusId != (int)EEstatusGuia.NO_VISITADO &&
            guia.EstatusId != (int)EEstatusGuia.REAGENDADO
        )
        throw new BusinessRuleException("El estatus que desea asignar no es correcto");
      guia.EstatusId = (int)EEstatusGuia.PREPARANDO_RUTA;
    }
  }
}
