namespace Tiui.Utils.Exceptions
{
    /// <summary>
    /// Clase para el manejo de las excepciones por autorización y/o autenticación
    /// </summary>
    public class AuthException:Exception
    {
        public AuthException(string message) : base(message)
        {
        }
    }
}
