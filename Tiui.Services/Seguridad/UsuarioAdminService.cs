using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tiui.Application.DTOs.Security;
using Tiui.Application.Repository.Seguridad;
using Tiui.Application.Security;
using Tiui.Application.Services.Seguidad;
using Tiui.Entities.Seguridad;

namespace Tiui.Services.Seguridad
{
    /// <summary>
    /// Servicio para el manejo de los usuarios
    /// </summary>
    public class UsuarioAdminService : IUsuarioAdminService
    {
        private readonly IUsarioRepository _usuarioRepository;
        private readonly IHashService _hashService;
        private readonly ISecurityManager _securityManager;
        private readonly IMapper _mapper;        
        /// <summary>
        /// Inicializa la instancia del servicio
        /// </summary>
        /// <param name="usuarioRepository">Repositorio para los usuarios</param>
        /// <param name="hashService">Servicio para generación de hash</param>
        public UsuarioAdminService(IUsarioRepository usuarioRepository, IHashService hashService, ISecurityManager securityManager, IMapper mapper)
        {
            this._usuarioRepository = usuarioRepository;
            this._hashService = hashService;
            this._securityManager = securityManager;
            this._mapper = mapper;        
        }

        public async Task<AuthenticatedAdminUserDTO> LoginTiui(LoginDTO loginDTO)
        {
            Usuario user = (await this._usuarioRepository.Query(u => u.NombreUsuario.Equals(loginDTO.UserName))).FirstOrDefault();
            if (user == null)
                return new AuthenticatedAdminUserDTO { MessageError = "El usuario no esta registrado en el sistema" };
            if (user.TipoUsuario != ETipoUsuario.ADMIN)
                return new AuthenticatedAdminUserDTO { MessageError = "El usuario no tiene permisos para acceder al sistema" };
            if (!this.PasswordValidate(user, loginDTO.Password))
                return new AuthenticatedAdminUserDTO { MessageError = "El Password es incorrecto" };            
            return this._securityManager.BuildAuthenticatedAdminUserObject(this._mapper.Map<UsuarioDTO>(user));
        }
        /// <summary>
        /// Valida la contraseña proporcionado por el usuario
        /// </summary>
        /// <param name="user">Usuario del sistema</param>
        /// <returns>True si la contraseña es correcta, en caso contrario false</returns>        
        private bool PasswordValidate(Usuario user, string password)
        {
            var salt = Convert.FromBase64String(user.Salt);
            var hashResult = this._hashService.GetHash(password, salt);
            return hashResult.Hash.Equals(user.Password);
        }

        /// <summary>
        /// Registra el usuario TiuiAmigo
        /// </summary>
        /// <param name="usuarioTiuiAmigo">DTO con la información a registrar</param>
        public async Task<AuthenticatedAdminUserDTO> RegistrarUsuarioTiuiAmigo(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo)
        {
            if (await this.IsRegisterUser(usuarioTiuiAmigo.CorreoElectronico))
                return new AuthenticatedAdminUserDTO { MessageError = "Ya existe un usuario registrado con el correo electrónico" };
            var resultHash = this._hashService.GetEncripHashResult(usuarioTiuiAmigo.Password);
            Usuario usuario = GetUser(usuarioTiuiAmigo, resultHash);
            var user = await this._usuarioRepository.CreateAdmin(usuario);
            return this._securityManager.BuildAuthenticatedAdminUserObject(this._mapper.Map<UsuarioDTO>(user));
        }
        /// <summary>
        /// Verifica si el usuario ya existe
        /// </summary>
        /// <param name="correoElectronico">Correo del usuario a validar</param>
        /// <returns>True si el usuario ya existe</returns>     
        private async Task<bool> IsRegisterUser(string correoElectronico)
        {
            return await this._usuarioRepository.Exists(correoElectronico);
        }
        /// <summary>
        /// Obtiene el usuario creaado
        /// </summary>
        /// <param name="usuarioTiuiAmigo">Contiene información con la que se creara el usuario</param>
        /// <param name="resultHash">Hash Generado para el usuario</param>
        /// <returns>Usuario creado</returns>
        private static Usuario GetUser(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo, EncripHashResult resultHash)
        {
            return new Usuario()
            {
                NombreCompleto = $"{usuarioTiuiAmigo.Nombre} {usuarioTiuiAmigo.Apellidos}",
                FechaModificacion = DateTime.UtcNow,
                Activo = true,
                NombreUsuario = usuarioTiuiAmigo.CorreoElectronico,
                TipoUsuario = ETipoUsuario.ADMIN,
                Password = resultHash.Hash,
                Salt = resultHash.Salt
            };
        }
    }
}
