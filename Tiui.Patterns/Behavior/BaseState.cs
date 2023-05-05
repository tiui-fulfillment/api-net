namespace Tiui.Patterns.Behavior
{
    /// <summary>
    /// Abstracción para el manejo de los state
    /// </summary>
    public abstract class BaseState
    {
        protected IBaseStateContext _context;
        public abstract int? GetId();
        public abstract string GetName();
        public abstract void Handle();
    }
}