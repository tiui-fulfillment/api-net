namespace Tiui.Security
{
    /// <summary>
    /// Configuración para el Json Web Token
    /// </summary>
    public class JwtSettings
    {     
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TimeToExpiration { get; set; }
    }
}
