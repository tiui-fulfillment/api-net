namespace Tiui.Utils.Paging
{
    /// <summary>
    /// Abstracción para la paginación del filtro
    /// </summary>
    public abstract class PageFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRows { get; set; }
    }
}
