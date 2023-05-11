using System.ComponentModel.DataAnnotations.Schema;

namespace Tiui.Entities.Comun
{
    /// <summary>
    /// Entidad para el manejo de los archivos en la aplicación (imagenes, documentos, etc)
    /// </summary>
    public class Archivo
    {
        [NotMapped]
        readonly string[] TIPOS_ARCHIVO = { ".png", ".jpg", ".pdf" };
        public long? ArchivoId { get; set; }
        public string Nombre { get; set; }
        public string NombreReal { get; set; }
        public string MimeType { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow.ToUniversalTime();
        public string Tag { get; set; }
        [NotMapped]
        public string Data { get; set; }
        /// <summary>
        /// Establece el nombre que tendra el archivo con el que sera guardado
        /// </summary>
        public void EstablecerNombreReal()
        {
            string fileName = Guid.NewGuid().ToString();
            var index = this.Nombre?.LastIndexOf(".") ?? 0;
            string extension = this.Nombre?.Substring(index) ?? "";
            if (this.TIPOS_ARCHIVO.FirstOrDefault(t => t.ToLower().Equals(extension.ToLower())) != null)
                this.NombreReal = fileName + extension;
            else
                this.NombreReal = fileName + "png";
        }
    }
}
