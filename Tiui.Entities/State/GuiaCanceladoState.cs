using System;
using Tiui.Entities.Cancelaciones;
using Tiui.Entities.Comun;
using Tiui.Entities.Guias;
using Tiui.Entities.Utils.Extensions;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
    /// <summary>
    /// Implementación del state GuiaCanceladoState
    /// </summary>
    public class GuiaCanceladoState : BaseState, ICancelState
    {
        private readonly MotivoCancelacion _motivoCancelacion;

        public GuiaCanceladoState(IBaseStateContext context, MotivoCancelacion motivoCancelacion)
        {
            if (context is Guia)
                this._context = context;
            else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
            if (motivoCancelacion == null)
                throw new ArgumentNullException("Se esperaba un motivo de cancelación para la guía");
            this._motivoCancelacion = motivoCancelacion;
        }

        public MotivoCancelacion MotivoCancelacion { get => this._motivoCancelacion; }

        /// <summary>
        /// Obtiene el identificador del status
        /// </summary>
        /// <returns>int que representa el identificador</returns>
        public override int? GetId()
        {
            return (int)EEstatusGuia.CANCELADO;
        }
        /// <summary>
        /// Obtiene el nombre del status
        /// </summary>
        /// <returns></returns>
        public override string GetName()
        {
            return "Cancelado";
        }
        /// <summary>
        /// Maneja el cambio de estatus de la guía
        /// </summary>
        /// <exception cref="InvalidOperationException">Produce una exception si no se puede realizar el cambio de estatus</exception>
        public override void Handle()
        {
            Guia guia = (Guia)this._context;
            if (
                guia.Estatus.Proceso.ToUpper().Equals(ETipoProceso.CELEBRANDO.GetString()) 
            || (guia.Estatus.Proceso.ToUpper().Equals(ETipoProceso.PREPARANDO.GetString()) && this._motivoCancelacion.TipoCancelacion != ETipoCancelacion.ANTES_DE_RUTA)
                || (guia.Estatus.Proceso.ToUpper().Equals(ETipoProceso.EN_CAMINO.GetString()) && this._motivoCancelacion.TipoCancelacion != ETipoCancelacion.EN_RUTA))
                throw new BusinessRuleException("El estatus que desea asignar no es correcto");
            guia.EstatusId = (int)EEstatusGuia.CANCELADO;
        }
    }
}
