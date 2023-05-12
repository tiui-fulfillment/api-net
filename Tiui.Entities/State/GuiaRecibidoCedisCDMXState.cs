using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
  /// <summary>
  /// Implementación del estatus GuiaRecibidoCedisCDMXState
  /// </summary>
  public class GuiaRecibidoCedisCDMXState : BaseState
  {
    public GuiaRecibidoCedisCDMXState(IBaseStateContext context)
    {
      if (context is Guia)
        this._context = context;
      else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
    }
    public override int? GetId()
    {
      return (int)EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX;
    }

    public override string GetName()
    {
      return "Tu envío se recibió en CEDIS Tiui CDMX";
    }
    /// <summary>
    /// Maneja el cambio de estatus de la guía
    /// </summary>
    /// <exception cref="InvalidOperationException">Produce una exception si no se puede realizar el cambio de estatus</exception>
    public override void Handle()
    {
      Guia guia = (Guia)this._context;
      if (
          guia.EstatusId != (int)EEstatusGuia.CAMINO_CEDIS_CDMX &&
          guia.EstatusId != (int)EEstatusGuia.ENVIO_PARA_REVISION
          )
        throw new BusinessRuleException("El estatus que desea asignar no es correcto");
      guia.EstatusId = (int)EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX;
    }
  }
}
