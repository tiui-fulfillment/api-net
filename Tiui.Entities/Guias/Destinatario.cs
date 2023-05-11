namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para la dirección del destinatario de la guía
    /// </summary>
    public class Destinatario : DireccionGuia
    {
        public Guia Guia { get; set; }
        public new long? GuiaId { get; set; }

        public string GetDireccion()
        {
            return $"{this.Calle}, #{this.Numero}, {this.Colonia}, {this.Ciudad}, {this.Estado}, {this.CodigoPostal}";
        }        
    }
}
