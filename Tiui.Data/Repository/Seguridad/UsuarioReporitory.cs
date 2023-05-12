using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tiui.Application.Repository.Seguridad;
using Tiui.Entities.Comun;
using Tiui.Entities.Seguridad;

namespace Tiui.Data.Repository.Seguridad
{
    /// <summary>
    /// Repositorio para el manejo de los usuarios
    /// </summary>
    public class UsuarioReporitory : CRUDRepository<Usuario>, IUsarioRepository
    {
        public UsuarioReporitory(TiuiDBContext context) : base(context)
        {

        }
        /// <summary>
        /// Crea un nuevo usuario para un tiui amigo
        /// </summary>
        /// <param name="usuario">Usuario con la información de la cuenta a crear</param>
        /// <param name="tiuiAmigo">TiuiAmigo con información del cliente a crear</param>
        /// <returns>Usuario creado</returns>
        public async Task<Usuario> Create(Usuario usuario, TiuiAmigo tiuiAmigo)
        {
            this._context.Usuarios.Add(usuario);
            this._context.TiuiAmigos.Add(tiuiAmigo);
            await this._context.SaveChangesAsync();            
            return usuario;
        }
        public async Task<Usuario> CreateAdmin(Usuario usuario)
        {
            this._context.Usuarios.Add(usuario);
            await this._context.SaveChangesAsync();
            return usuario;
        }

        /// <summary>
        /// Verifica si el correo electrónico pertenece a un usuario registrado
        /// </summary>
        /// <param name="correoElectronico">Correo electrónico a validar</param>
        /// <returns>True si el usuario ya esta registrado en el sistema, False en caso contrario</returns>       
        public async Task<bool> Exists(string correoElectronico)
        {
            return await this._context.Usuarios.AnyAsync(u => u.NombreUsuario.ToLower().Equals(correoElectronico.ToLower()));
        }
    }
}
