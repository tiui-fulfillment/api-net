using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tiui.Entities.Comun;
using Tiui.Patterns.Behavior;

namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para el manejo de las guias
    /// </summary>
    public class Guia : IBaseStateContext
    {
        public long? GuiaId { get; set; }
        public bool EsPagoContraEntrega { get; set; }
        public decimal ImporteContraEntrega { get; set; }
        public bool TieneSeguroMercancia { get; set; }
        public decimal ImporteCalculoSeguro { get; set; }
        public decimal ImporteSeguroMercancia { get; set; }
        public decimal ImportePaqueteria { get; set; }
        public decimal CostoOperativo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public DateTime? FechaRegistro { get; set; } = DateTime.UtcNow;
        public Remitente Remitente { get; set; }
        public Destinatario Destinatario { get; set; }
        public Paquete Paquete { get; set; }
        public long? PaqueteId { get; set; }
        public TiuiAmigo TiuiAmigo { get; set; }
        public int? TiuiAmigoId { get; set; }
        public Paqueteria Paqueteria { get; set; }
        public int? PaqueteriaId { get; set; }
        public decimal CobroContraEntrega { get; set; }
        public int CantidadPaquetes { get; set; }
        public string NombreProducto { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public int Consecutivo { get; set; }
        public Estatus Estatus { get; set; }
        public int? EstatusId { get; set; }
        public ETipoProcesoCancelacion? TipoProcesoCancelacion { get; set; }
        public string ProcesoCancelacion { get; set; }
        public DateTime? FechaReagendado { get; set; }
        public DateTime? FechaConciliacion { get; set; }
        public bool? EsDevolucion { get; set; }
        public bool? RequiereVerificacion { get; set; }
        public string? FolioDevolucion { get; set; }

        /// <summary>
        /// Genera y establece el folio para la guía actual
        /// </summary>
        /// <param name="codigo">Codigo del cliente</param>
        /// <param name="aleatorio">Número aleatorio</param>
        /// <param name="consecutivo">Número consecutivo</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetFollio(string codigo, int random, int consecutivo)
        {
            this.Folio = $"{codigo}-{random.ToString("00")}-{consecutivo.ToString("0000")}";
        }
        [NotMapped]
        public BaseState CurrenteState { get; set; }
        /// <summary>
        /// Realiza el cambio de estatus de la guía mediante el BaseState asignado
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void ChangeState()
        {
            if (this.CurrenteState == null)
                throw new InvalidOperationException("No se ha establecido el manejador para el nuevo estatus de la guía");
            if (this.EstatusId == this.CurrenteState.GetId())
                throw new InvalidOperationException("El estatus de la guía no ha cambiado");
            this.CurrenteState.Handle();
        }
    }
}
