namespace Tiui.Utils.Exceptions
{
    /// <summary>
    /// Clase para el manejo de las excepciones en las reglas de negocio
    /// </summary>
    public class BusinessRuleException : Exception
    {
        /// <summary>
        /// Crea una instancia de la case
        /// </summary>
        /// <param name="message"></param>
        public BusinessRuleException(string message) : base(message)
        {
        }
    }
}
