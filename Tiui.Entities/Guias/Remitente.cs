﻿namespace Tiui.Entities.Guias
{
    /// <summary>
    /// Entidad para la dirección del remitente de la guía
    /// </summary>
    public class Remitente : DireccionGuia
    {       
        public Guia Guia { get; set; }
        public new long? GuiaId { get; set; }
    }
}
