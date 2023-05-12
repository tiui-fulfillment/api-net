using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
  /// <summary>
  /// Implementación del state GuiaPrimerIntentoEntregaState
  /// </summary>
  public class GuiaPrimerIntentoEntregaState : BaseState
  {
    public GuiaPrimerIntentoEntregaState(IBaseStateContext context)
    {
      if (context is Guia)
        this._context = context;
      else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
    }
    /// <summary>
    /// Obtiene el identificador del status
    /// </summary>
    /// <returns>int que representa el identificador</returns>
    public override int? GetId()
    {
      return (int)EEstatusGuia.PRIMER_INTENTO_ENTREGA;
    }
    /// <summary>
    /// Obtiene el nombre del status
    /// </summary>
    /// <returns></returns>
    public override string GetName()
    {
      return "Se realizó el 1er intento de entrega";
    }
    /// <summary>
    /// Maneja el cambio de estatus de la guía
    /// </summary>
    /// <exception cref="InvalidOperationException">Produce una exception si no se puede realizar el cambio de estatus</exception>
    public override void Handle()
    {
      Guia guia = (Guia)this._context;
      if (
          guia.EstatusId != (int)EEstatusGuia.RUTA_ENTREGA
      )
        throw new BusinessRuleException("El estatus que desea asignar no es correcto");
      guia.EstatusId = (int)EEstatusGuia.PRIMER_INTENTO_ENTREGA;
    }
  }
}
