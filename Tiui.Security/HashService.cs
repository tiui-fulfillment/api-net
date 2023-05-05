using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Tiui.Application.Security;

namespace Tiui.Security
{
    /// <summary>
    /// Servicio para la generación de hash de seguridad
    /// </summary>
    public class HashService : IHashService
    {
        public EncripHashResult GetEncripHashResult(string password)
        {
            var sal = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(sal);
            }
            return GetHash(password ?? String.Empty, sal);
        }
        /// <summary>
        /// Obtiene la clave encriptada con su salt
        /// </summary>
        /// <param name="password">Dato a encriptar</param>
        /// <param name="sal">Semilla de encriptado</param>
        /// <returns>EncripHashResult con la información cifrada</returns>
        public EncripHashResult GetHash(string password, byte[] sal)
        {
            var key = KeyDerivation.Pbkdf2(password: password, salt: sal, prf: KeyDerivationPrf.HMACSHA1
                , iterationCount: 10000, numBytesRequested: 32);
            var hash = Convert.ToBase64String(key);
            var salt = Convert.ToBase64String(sal);
            return new EncripHashResult { Hash = hash, Salt = salt };
        }
    }
}
