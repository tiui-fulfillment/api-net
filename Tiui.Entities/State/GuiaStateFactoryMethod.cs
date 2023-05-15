using System;
using Tiui.Entities.Cancelaciones;
using Tiui.Entities.Guias;
using Tiui.Patterns.Behavior;

namespace Tiui.Entities.State
{
    /// <summary>
    /// Genera manejador para el tipo de estatus que se aplicará a la guía
    /// </summary>
    public static class GuiaStateFactoryMethod
    {
        /// <summary>
        /// Crea una instancia del BaseState que permite aplicar un nuevo estatus a la guía
        /// </summary>
        /// <param name="guia"></param>
        /// <param name="estatusGuia"></param>
        /// <param name="fechaReagendado"></param>
        /// <param name="fechaConciliado"></param>
        /// <returns>BaseState para en manejo del estatus de la guía</returns>
        public static BaseState CreateGuiaState(Guia guia, EEstatusGuia estatusGuia, DateTime? fechaReagendado = null
            , DateTime? fechaConciliado = null, MotivoCancelacion motivoCancelacion = null)
        {
            BaseState baseState;
            switch (estatusGuia)
            {
                case EEstatusGuia.CAMINO_CEDIS_CDMX:
                    baseState = new GuiaCaminoCedisCDMXState(guia);
                    break;
                case EEstatusGuia.RECIBIDO_EN_CEDIS_CDMX:
                    baseState = new GuiaRecibidoCedisCDMXState(guia);
                    break;
                case EEstatusGuia.EN_CEDIS_CDMX:
                    baseState = new GuiaEnCedisCDMXState(guia);
                    break;
                case EEstatusGuia.PREPARACION:
                    baseState = new GuiaPreparacionState(guia);
                    break;
                case EEstatusGuia.FULFILLMENT_EN_CEDIS_CDMX:
                    baseState = new GuiaFulfillmentEnCedisCDMXState(guia);
                    break;
                case EEstatusGuia.PRODUCTO_AGOTADO:
                    baseState = new GuiaProductoAgotadoState(guia);
                    break;
                case EEstatusGuia.RUTA_ENTREGA:
                    baseState = new GuiaRutaEntregaState(guia);
                    break;
                case EEstatusGuia.PRIMER_INTENTO_ENTREGA:
                    baseState = new GuiaPrimerIntentoEntregaState(guia);
                    break;
                case EEstatusGuia.REAGENDADO:
                    baseState = new GuiaReagendadoState(guia, fechaReagendado);
                    break;
                case EEstatusGuia.CANCELADO:
                    baseState = new GuiaCanceladoState(guia, motivoCancelacion);
                    break;
                case EEstatusGuia.ENTREGAGO:
                    baseState = new GuiaEntregadoState(guia);
                    break;
                case EEstatusGuia.CONCILIADO:
                    baseState = new GuiaConciliadoState(guia, fechaConciliado);
                    break;
                case EEstatusGuia.PAGADO:
                    baseState = new GuiaPagadoState(guia);
                    break;
                case EEstatusGuia.EN_CAMINO_RECOLECCION:
                    baseState = new GuiaEnCaminoRecoleccionState(guia);
                    break;
                case EEstatusGuia.ENVIO_RECOLECTADO:
                    baseState = new GuiaEnvioRecolectadoState(guia);
                    break;
                case EEstatusGuia.EN_CEDIS_CDMX_RECOLECCION:
                    baseState = new GuiaRecoleccionEnCedisCDMXState(guia);
                    break;
                case EEstatusGuia.ENVIO_PARA_REVISION:
                    baseState = new GuiaEnvioParaRevisionState(guia);
                    break;
                case EEstatusGuia.NO_VISITADO:
                    baseState = new GuiaNoVisitadoState(guia);
                    break;
                case EEstatusGuia.PREPARANDO_RUTA:
                    baseState = new GuiaPreparcionDeRutaState(guia);
                    break;
                case EEstatusGuia.EN_ESPERA_DEVOLUCION:
                    baseState = new GuiaEsperaDevolucionState(guia);
                    break;
                case EEstatusGuia.DEVUELTO:
                    baseState = new GuiaDevueltoState(guia);
                    break;
                case EEstatusGuia.RECOLECCION_NO_VISITADO:
                    baseState = new GuiaRecoleccionNoVisitadoState(guia);
                    break;
                case EEstatusGuia.RECOLECCION_REPROGRAMADA:
                    baseState = new GuiaRecoleccionReprogramadaState(guia, fechaReagendado);
                    break;
                case EEstatusGuia.INTENTO_RECOLECCION:
                    baseState = new GuiaIntentoRecoleccionState(guia);
                    break;
                default:
                    throw new InvalidOperationException("El estatus especificado no es válido");
            }
            guia.CurrenteState = baseState;
            return baseState;
        }
    }
}
