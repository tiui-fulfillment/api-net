using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
  /// <summary>
  /// Implementación del estatus GuiaEnCaminoRecoleccionState
  /// </summary>
  public class GuiaEnCaminoRecoleccionState : BaseState
  {
    public GuiaEnCaminoRecoleccionState(IBaseStateContext context)
    {
      if (context is Guia)
        this._context = context;
      else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
    }
    public override int? GetId()
    {
      return (int)EEstatusGuia.EN_CAMINO_RECOLECCION;
    }

    public override string GetName()
    {
      return "En camino a recolección";
    }
    /// <summary>
    /// Maneja el cambio de estatus de la guía
    /// </summary>
    /// <exception cref="InvalidOperationException">Produce una exception si no se puede realizar el cambio de estatus</exception>
    public override void Handle()
    {
      Guia guia = (Guia)this._context;
      if (
            guia.EstatusId != (int)EEstatusGuia.DOCUMENTADO_RECOLECCION &&
            guia.EstatusId != (int)EEstatusGuia.ENVIO_PARA_REVISION
            )
        throw new BusinessRuleException("El estatus que desea asignar no es correcto");
      guia.EstatusId = (int)EEstatusGuia.EN_CAMINO_RECOLECCION;
    }
  }
}
