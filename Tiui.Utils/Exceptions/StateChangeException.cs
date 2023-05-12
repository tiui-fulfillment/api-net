using System;

namespace Tiui.Utils.Exceptions
{
    /// <summary>
    /// Clase para el manejo de las excepciones en las reglas de negocio
    /// </summary>
    public class StateChangeException : Exception
    {
        /// <summary>
        /// Crea una instancia de la case
        /// </summary>
        /// <param name="message"></param>
        public StateChangeException(string message) : base(message)
        {
        }
    }
}
