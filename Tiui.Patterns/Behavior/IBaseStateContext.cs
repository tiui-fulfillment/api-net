namespace Tiui.Patterns.Behavior
{
    /// <summary>
    /// Abstracción para el manejo del context client
    /// </summary>
    public interface IBaseStateContext
    {
        BaseState CurrenteState { get; set; }
        public void ChangeState();
    }
}
