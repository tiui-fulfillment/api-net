namespace Tiui.Application.DTOs
{
    /// <summary>
    /// Clase generica para el manejo de resultado del api
    /// </summary>
    public class ApiResultModel<T>
    {
        public T Entity { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

    }
}
