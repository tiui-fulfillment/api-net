using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
    /// <summary>
    /// Implementación del state GuiaReagendadoState
    /// </summary>
    public class GuiaReagendadoState : BaseState
    {
        private readonly DateTime? _fechaReagendado;

        public GuiaReagendadoState(IBaseStateContext context, DateTime? fechaReagendado)
        {
            if (context is Guia)
                this._context = context;
            else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
            this._fechaReagendado = fechaReagendado;
        }
        /// <summary>
        /// Obtiene el identificador del status
        /// </summary>
        /// <returns>int que representa el identificador</returns>
        public override int? GetId()
        {
            return (int)EEstatusGuia.REAGENDADO;
        }
        /// <summary>
        /// Obtiene el nombre del status
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return $"Tu envío se reagendó para {this._fechaReagendado}";
        }
        /// <summary>
        /// Maneja el cambio de estatus de la guía
        /// </summary>
        /// <exception cref="InvalidOperationException">Produce una exception si no se puede realizar el cambio de estatus</exception>
        public override void Handle()
        {
            Guia guia = (Guia)this._context;
            if (
                guia.EstatusId != (int)EEstatusGuia.PRIMER_INTENTO_ENTREGA &&
                guia.EstatusId != (int)EEstatusGuia.REAGENDADO &&
                guia.EstatusId != (int)EEstatusGuia.ENVIO_PARA_REVISION &&
                guia.EstatusId != (int)EEstatusGuia.PREPARANDO_RUTA &&
                guia.EstatusId != (int)EEstatusGuia.NO_VISITADO
                )
                throw new BusinessRuleException("El estatus que desea asignar no es correcto");
            guia.EstatusId = (int)EEstatusGuia.REAGENDADO;
            guia.FechaReagendado = this._fechaReagendado;
        }
    }
}
