using Tiui.Entities.Comun;

namespace Tiui.Entities.Utils.Extensions
{
    /// <summary>
    /// Clase utilitaria para extender funciones de los estatus que se manejan en el sistema 
    /// </summary>
    public static class StatusHelpersExtensions
    {
        public static string GetString(this ETipoProceso status)
        {
            switch (status)
            {
                case ETipoProceso.PREPARANDO:
                    return "PREPARANDO";
                case ETipoProceso.EN_CAMINO:
                    return "EN CAMINO";
                case ETipoProceso.CELEBRANDO:
                    return "CELEBRANDO";
                default:
                    return status.ToString();
            }
        }
    }
}
