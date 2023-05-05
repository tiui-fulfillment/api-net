using Tiui.Entities.Cancelaciones;

namespace Tiui.Entities.State
{
    /// <summary>
    /// Abstracción para adicionar datos a las cancelaciones
    /// </summary>
    public interface ICancelState
    {
        public MotivoCancelacion MotivoCancelacion { get; }
    }
}
