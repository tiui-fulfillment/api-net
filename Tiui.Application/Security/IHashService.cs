namespace Tiui.Application.Security
{
    /// <summary>
    /// Abstracción para la generación de hash seguros
    /// </summary>
    public interface IHashService
    {
        EncripHashResult GetEncripHashResult(string password);
        EncripHashResult GetHash(string password, byte[] sal);
    }
}
