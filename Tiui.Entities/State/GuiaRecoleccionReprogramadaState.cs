using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
  /// <summary>
  /// Implementación del estatus GuiaRecoleccionReprogramadaState
  /// </summary>
  public class GuiaRecoleccionReprogramadaState : BaseState
  {
    private readonly DateTime? _fechaReagendado;
    public GuiaRecoleccionReprogramadaState(IBaseStateContext context, DateTime? fechaReagendado)
    {
      if (context is Guia)
        this._context = context;
      else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
      this._fechaReagendado = fechaReagendado;
    }
    public override int? GetId()
    {
      return (int)EEstatusGuia.RECOLECCION_REPROGRAMADA;
    }

    public override string GetName()
    {
      return "Recolección reprogramada";
    }
    /// <summary>
    /// Maneja el cambio de estatus de la guía
    /// </summary>
    /// <exception cref="BusinessRuleException">Produce una exception si no se puede realizar el cambio de estatus</exception>
    public override void Handle()
    {
      Guia guia = (Guia)this._context;
      if (
          guia.EstatusId != (int)EEstatusGuia.RECOLECCION_NO_VISITADO &&
          guia.EstatusId != (int)EEstatusGuia.INTENTO_RECOLECCION &&
          guia.EstatusId != (int)EEstatusGuia.RECOLECCION_REPROGRAMADA &&
          guia.EstatusId != (int)EEstatusGuia.ENVIO_PARA_REVISION &&
          guia.EstatusId != (int)EEstatusGuia.EN_CAMINO_RECOLECCION
          )
        throw new BusinessRuleException("El estatus que desea asignar no es correcto");
      guia.EstatusId = (int)EEstatusGuia.RECOLECCION_REPROGRAMADA;
      guia.FechaReagendado = this._fechaReagendado;
    }
  }
}
