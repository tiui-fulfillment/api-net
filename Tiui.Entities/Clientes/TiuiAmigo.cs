using Tiui.Entities.Comun;

namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de los clientes tiui amigos
    /// </summary>
    public class TiuiAmigo
    {
        public int? TiuiAmigoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string CorreoElectronico { get; set; }
        public Direccion Direccion { get; set; }
        public long? DireccionId { get; set; }
        public string Celular { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public Direccion DireccionFiscal { get; set; }
        public long? DireccionFiscalId { get; set; }
        public string TelefonoContacto { get; set; }
        public string NombreContacto { get; set; }
        public Archivo FotoINE { get; set; }
        public long? ArchivoId { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; }
        public string Codigo { get; set; }
        public ETipoFlujoGuia TipoProceso { get; set; }
        /// <summary>
        /// Obtiene el nombre completo del tiui amigo
        /// </summary>
        /// <returns>Cadena con el nombre completo del tiui amigo</returns>
        public string GetNombreCompleto()
        {
            return $"{Nombres} {Apellidos}";
        }
        public void SetCode(string sequence)
        {
            int valor = 1;
            if (!string.IsNullOrEmpty(sequence))
                valor = int.Parse(sequence) + 1;
            this.Codigo = valor.ToString("00");

        }
    }
}
