using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tiui.Application.DTOs;
using Tiui.Application.DTOs.Security;
using Tiui.Application.Mail.Configuration;
using Tiui.Application.Repository.Clientes;
using Tiui.Application.Repository.Seguridad;
using Tiui.Application.Repository.UnitOfWork;
using Tiui.Application.Security;
using Tiui.Application.Services.Seguidad;
using Tiui.Entities.Comun;
using Tiui.Entities.Seguridad;
using Tiui.Utils.Exceptions;

namespace Tiui.Services.Seguridad
{
    /// <summary>
    /// Servicio para el manejo de los usuarios
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsarioRepository _usuarioRepository;
        private readonly IHashService _hashService;
        private readonly ISecurityManager _securityManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INuevoTiuiAmigoEmail _nuevoAmigoTiuiEmail;
        private readonly IConfiguration _configuracion;
        private readonly ILogger<UsuarioService> _logger;
        private readonly ITiuiAmigoRepository _tiuiAmigoRepository;
        private readonly IRecuperarContraseñaEmail _recuperarContraseñaEmail;

        /// <summary>
        /// Inicializa la instancia del servicio
        /// </summary>
        /// <param name="usuarioRepository">Repositorio para los usuarios</param>
        /// <param name="hashService">Servicio para generación de hash</param>
        public UsuarioService(IUsarioRepository usuarioRepository, IHashService hashService, ISecurityManager securityManager, IMapper mapper
            , IUnitOfWork unitOfWork, INuevoTiuiAmigoEmail nuevoAmigoTiuiEmail, IConfiguration configuracion, ILogger<UsuarioService> logger
            , ITiuiAmigoRepository tiuiAmigoRepository, IRecuperarContraseñaEmail recuperarContraseñaEmail)
        {
            this._usuarioRepository = usuarioRepository;
            this._hashService = hashService;
            this._securityManager = securityManager;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._nuevoAmigoTiuiEmail = nuevoAmigoTiuiEmail;
            this._configuracion = configuracion;
            this._logger = logger;
            this._tiuiAmigoRepository = tiuiAmigoRepository;
            this._recuperarContraseñaEmail = recuperarContraseñaEmail;
        }

        public async Task<AuthenticatedUserDTO> LoginTiui(LoginDTO loginDTO)
        {
            Usuario user = (await this._usuarioRepository.Query(u => u.NombreUsuario.Equals(loginDTO.UserName))).FirstOrDefault();
            if (user == null)
                return new AuthenticatedUserDTO { MessageError = "El usuario no esta registrado en el sistema" };
            if (user.TipoUsuario != ETipoUsuario.AMIGOTIUI)
                return new AuthenticatedUserDTO { MessageError = "El usuario no tiene permisos para acceder al sistema" };
            if (!this.PasswordValidate(user, loginDTO.Password))
                return new AuthenticatedUserDTO { MessageError = "El Password es incorrecto" };
            TiuiAmigo tiuiAmigo = (await this._unitOfWork.Repository<TiuiAmigo>().Query(t => t.CorreoElectronico.Equals(loginDTO.UserName))).FirstOrDefault();
            return this._securityManager.BuildAuthenticatedUserObject(this._mapper.Map<UsuarioDTO>(user), tiuiAmigo.TiuiAmigoId, this.GetSessionId(user));
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
        public async Task<AuthenticatedUserDTO> RefreshToken(RefreshTokenDTO refreshToken)
        {
            Usuario user = (await this._usuarioRepository.Query(u => u.UsuarioId == refreshToken.UserId)).FirstOrDefault();
            if (user == null)
                return new AuthenticatedUserDTO { MessageError = "El usuario no esta registrado en el sistema" };
            if (user.TipoUsuario != ETipoUsuario.AMIGOTIUI)
                return new AuthenticatedUserDTO { MessageError = "El usuario no tiene permisos para acceder al sistema" };
            if (!refreshToken.SessionId.Equals(this.GetSessionId(user)))
                return new AuthenticatedUserDTO { MessageError = "El id de sesión no es valido" };
            TiuiAmigo tiuiAmigo = (await this._unitOfWork.Repository<TiuiAmigo>().Query(t => t.CorreoElectronico.Equals(refreshToken.UserName))).FirstOrDefault();
            return this._securityManager.BuildAuthenticatedUserObject(this._mapper.Map<UsuarioDTO>(user), tiuiAmigo.TiuiAmigoId, refreshToken.SessionId);
        }
        private string GetSessionId(Usuario user)
        {
            var salt = Convert.FromBase64String(user.Salt);
            var hashResult = this._hashService.GetHash($"{user.UsuarioId}{user.NombreUsuario}", salt);
            return hashResult.Hash;
        }

        /// <summary>
        /// Registra el usuario TiuiAmigo
        /// </summary>
        /// <param name="usuarioTiuiAmigo">DTO con la información a registrar</param>
        public async Task<AuthenticatedUserDTO> RegistrarUsuarioTiuiAmigo(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo)
        {
            if (await this.IsRegisterUser(usuarioTiuiAmigo.CorreoElectronico))
                return new AuthenticatedUserDTO { MessageError = "Ya existe un usuario registrado con el correo electrónico" };
            var resultHash = this._hashService.GetEncripHashResult(usuarioTiuiAmigo.Password);
            TiuiAmigo tiuiAmigo = await GetTiuiAmigo(usuarioTiuiAmigo);
            Usuario usuario = GetUser(usuarioTiuiAmigo, resultHash);
            var user = await this._usuarioRepository.Create(usuario, tiuiAmigo);
            this.SendMail(usuario);
            return this._securityManager.BuildAuthenticatedUserObject(this._mapper.Map<UsuarioDTO>(user), tiuiAmigo.TiuiAmigoId);
        }
        /// <summary>
        /// Envía notificación al nuevo tiui amigo que su cuenta ha sido creada
        /// </summary>
        /// <param name="usuario">Usuario que contiene la información</param>
        /// <returns>True si el envío de correo fue exitoso</returns>       
        private void SendMail(Usuario usuario)
        {
            try
            {
                this._nuevoAmigoTiuiEmail.To = usuario.NombreUsuario;
                this._nuevoAmigoTiuiEmail.TiuAmigo(usuario.NombreCompleto).UrlWebSite(this._configuracion["UrlWebSite"]);
                this._nuevoAmigoTiuiEmail.SendMailAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
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
                FechaModificacion = DateTime.Now,
                Activo = true,
                NombreUsuario = usuarioTiuiAmigo.CorreoElectronico,
                TipoUsuario = ETipoUsuario.AMIGOTIUI,
                Password = resultHash.Hash,
                Salt = resultHash.Salt
            };
        }
        /// <summary>
        /// Obtiene la información del del tiuiAmigo
        /// </summary>
        /// <param name="usuarioTiuiAmigo">Contiene la información para derivar el tiuiamigo</param>
        /// <returns>TuiaAmigo a registrar</returns>
        private async Task<TiuiAmigo> GetTiuiAmigo(UsuarioTiuiAmigoCreateDTO usuarioTiuiAmigo)
        {
            if (!Enum.IsDefined(typeof(ETipoFlujoGuia), usuarioTiuiAmigo.TipoProceso))
                throw new BusinessRuleException("El tipo de proceso proporcionado no es correcto");
            TiuiAmigo tiuiAmigo = new TiuiAmigo
            {
                Nombres = usuarioTiuiAmigo.Nombre,
                Apellidos = usuarioTiuiAmigo.Apellidos,
                CorreoElectronico = usuarioTiuiAmigo.CorreoElectronico,
                TelefonoContacto = usuarioTiuiAmigo.Telefono,
                Activo = true,
                TipoProceso = (ETipoFlujoGuia)usuarioTiuiAmigo.TipoProceso
            };
            var sequence = await this._tiuiAmigoRepository.GetSequence();
            tiuiAmigo.SetCode(sequence);
            return tiuiAmigo;
        }
        public async Task<ApiResultModel<UsuarioDTO>> GeneratePasswordRecoveryCode(UserRecoveryPasswordDTO loginDTO)
        {
            var user = (await this._usuarioRepository.Query(u => u.NombreUsuario == loginDTO.UserName)).FirstOrDefault();
            if (user == null)
                return new ApiResultModel<UsuarioDTO>() { Message = "El usuario no existe", Status = "404", Success = false };
            Random random = new Random();
            user.CodigoReestablecerPassword = random.Next(1, 9999999);
            this._usuarioRepository.Update(user);
            await this._usuarioRepository.Commit();
            this.SendMailCode(user);
            return this.GetUserDTOResultOk(user, "El código para reestablecer su contraseña ha sido envíado a su cuenta de correo electrónico.");
        }
        private void SendMailCode(Usuario user)
        {
            try
            {
                this._recuperarContraseñaEmail.To = user.NombreUsuario;
                this._recuperarContraseñaEmail.Codigo(user.CodigoReestablecerPassword.ToString());
                this._recuperarContraseñaEmail.SendMailAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
            }
        }
        public async Task<ApiResultModel<UsuarioDTO>> ValidatePasswordRecoveryCode(UserRecoveryPasswordDTO loginDTO)
        {
            var user = await this.ValidatePasswordRecoveryData(loginDTO);
            return GetUserDTOResultOk(user, "Validación exitosa");
        }
        public async Task<ApiResultModel<UsuarioDTO>> PasswordChange(UserRecoveryPasswordDTO loginDTO)
        {
            loginDTO.ChangePassword = true;
            var user = await this.ValidatePasswordRecoveryData(loginDTO);
            var resultHash = this._hashService.GetEncripHashResult(loginDTO.Password);
            user.Password = resultHash.Hash;
            user.Salt = resultHash.Salt;
            user.CodigoReestablecerPassword = 0;
            this._usuarioRepository.Update(user);
            await this._usuarioRepository.Commit();
            return GetUserDTOResultOk(user, "La contraseña ha sido cambiada");
        }
        private async Task<Usuario> ValidatePasswordRecoveryData(UserRecoveryPasswordDTO loginDTO)
        {
            if (loginDTO.ChangePassword && string.IsNullOrEmpty(loginDTO.Password))
                throw new BusinessRuleException("El password no es válido");
            var user = (await this._usuarioRepository.Query(u => u.UsuarioId == loginDTO.UserId)).FirstOrDefault();
            if (user == null)
                throw new BusinessRuleException("El usuario no existe");
            if (user.CodigoReestablecerPassword == 0 || user.CodigoReestablecerPassword != loginDTO.Code)
                throw new BusinessRuleException("El código no es válido");
            return user;
        }
        private ApiResultModel<UsuarioDTO> GetUserDTOResultOk(Usuario user, string message)
        {
            return new ApiResultModel<UsuarioDTO>
            {
                Message = message,
                Status = "200",
                Success = true,
                Entity = this._mapper.Map<UsuarioDTO>(user)
            };
        }        
    }
}
