namespace Tiui.Application.Mail.Configuration
{
    /// <summary>
    /// Abstracción para el manejo de correos para los nuevos usuarios tiuiamigo
    /// </summary>
    public interface INuevoTiuiAmigoEmail:IConfiguracionEmail
    {      
        INuevoTiuiAmigoEmail TiuAmigo(string tiuiAmigo);
        INuevoTiuiAmigoEmail UrlWebSite(string url);    
    }
}
