namespace Tiui.Application.File
{
    public interface IFileService
    {
        /// <summary>
        /// Genera un Zip en Memoria a partir de una lista de direcciones físicas
        /// </summary>
        /// <param name="pathFileList">Lista de direcciones físicas de archivos</param>
        /// <returns>Zip</returns>
        MemoryStream GenerarZip(List<string> pathFileList);
        /// <summary>
        /// Función que genera un archivo Zip en Memoria
        /// </summary>
        /// <param name="files">Diccionario de datos con <NombreArchivo.extension,MemoryStream></param>
        /// <param name="pathFileList">Lista de direcciones físicas de archivos</param>
        /// <returns>Secuencia de Bytes correspondientes al archivo Zip Generado</returns>
        byte[] GenerarZip(Dictionary<string, MemoryStream> files, List<string> pathFileList);
  /*       byte[] GenerateQrCode(string qrmsg); */
    }
}
