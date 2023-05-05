using ICSharpCode.SharpZipLib.Zip;
/* using QRCoder; */
using System.Drawing;
using System.Drawing.Imaging;
using Tiui.Application.File;

namespace Tiui.Files
{
    public class FileService:IFileService
    {
        /// <summary>
        /// Genera un Zip en Memoria a partir de una lista de direcciones físicas
        /// </summary>
        /// <param name="pathFileList">Lista de direcciones físicas de archivos</param>
        /// <returns>Zip</returns>
        public MemoryStream GenerarZip(List<string> pathFileList)
        {
            MemoryStream ms = new MemoryStream();

            ZipOutputStream stream = new ZipOutputStream(ms);
            stream.SetLevel(5);

            for (int i = 0; i < pathFileList.Count; i++)
            {
                if (!File.Exists(pathFileList[i]))
                    continue;

                ZipEntry entry = new ZipEntry(Path.GetFileName(pathFileList[i]));
                entry.DateTime = DateTime.Now;

                using (FileStream fs = File.OpenRead(pathFileList[i]))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry.Size = fs.Length;
                    fs.Close();

                    stream.PutNextEntry(entry);
                    stream.Write(buffer, 0, buffer.Length);
                    stream.CloseEntry();
                }
            }

            stream.IsStreamOwner = false;
            stream.Finish();
            stream.Close();

            ms.Position = 0;

            return ms;
        }

        /// <summary>
        /// Función que genera un archivo Zip en Memoria
        /// </summary>
        /// <param name="files">Diccionario de datos con <NombreArchivo.extension,MemoryStream></param>
        /// <param name="pathFileList">Lista de direcciones físicas de archivos</param>
        /// <returns>Secuencia de Bytes correspondientes al archivo Zip Generado</returns>
        public byte[] GenerarZip(Dictionary<string, MemoryStream> files, List<string> pathFileList)
        {
            byte[] result = null;

            using (MemoryStream ms = new MemoryStream())
            {
                ZipOutputStream stream = new ZipOutputStream(ms);
                stream.SetLevel(5);

                if (pathFileList != null)
                {
                    for (int i = 0; i < pathFileList.Count; i++)
                    {
                        if (!File.Exists(pathFileList[i]))
                            continue;

                        ZipEntry entry = new ZipEntry(Path.GetFileName(pathFileList[i]));
                        entry.DateTime = DateTime.Now;

                        using (FileStream fs = File.OpenRead(pathFileList[i]))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            entry.Size = fs.Length;
                            fs.Close();

                            stream.PutNextEntry(entry);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.CloseEntry();
                        }
                    }
                }

                if (files != null)
                {
                    foreach (KeyValuePair<string, MemoryStream> elemento in files)
                    {
                        ZipEntry entry = new ZipEntry(elemento.Key);
                        entry.DateTime = DateTime.Now;

                        using (MemoryStream entryStream = new MemoryStream())
                        {
                            elemento.Value.CopyTo(entryStream);
                            entryStream.Position = 0;

                            byte[] buffer = new byte[entryStream.Length];
                            entryStream.Read(buffer, 0, buffer.Length);
                            entry.Size = entryStream.Length;
                            entryStream.Close();

                            stream.PutNextEntry(entry);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.CloseEntry();
                        }
                    }
                }

                stream.IsStreamOwner = false;
                stream.Finish();
                stream.Close();

                ms.Position = 0;

                result = ms.ToArray();
            }

            return result;
        }

 /*        public byte[] GenerateQrCode(string qrmsg)
        {
       
        } */
    }
}
