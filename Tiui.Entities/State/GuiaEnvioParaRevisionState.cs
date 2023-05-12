using System;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;
using Tiui.Utils.Exceptions;

namespace Tiui.Entities.State
{
    /// <summary>
    /// Implementación del estatus GuiaEnvioParaRevisionState
    /// </summary>
    public class GuiaEnvioParaRevisionState : BaseState
    {
        public GuiaEnvioParaRevisionState(IBaseStateContext context)
        {
            if (context is Guia)
                this._context = context;
            else throw new ArgumentNullException("Se esperaba una Guia para el contexto");
        }
        public override int? GetId()
        {
            return (int)EEstatusGuia.ENVIO_PARA_REVISION;
        }

        public override string GetName()
        {
            return "Envio para revision";
        }
        /// <summary>
        /// Maneja el cambio de estatus de la guía
        /// </summary>
        /// <exception cref="BusinessRuleException">Produce una exception si no se puede realizar el cambio de estatus</exception>
        public override void Handle()
        {
            Guia guia = (Guia)this._context;
            guia.EstatusId = (int)EEstatusGuia.ENVIO_PARA_REVISION;
        }
    }
}
