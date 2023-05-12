using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tiui.Application.DTOs.Security;
using Tiui.Application.Security;

namespace Tiui.Security
{
    /// <summary>
    /// Administrador para verificar la seguridad de acceso a las apis
    /// </summary>
    public class SecurityManager : ISecurityManager
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public SecurityManager(IConfiguration configuration, JwtSettings jwtSettings)
        {
            this._configuration = configuration;
            this._jwtSettings = jwtSettings;
        }
        /// <summary>
        /// Retorna información del usuario logeado
        /// </summary>
        /// <param name="user">Usuario logeado</param>
        /// <returns>AuthenticatedUserDTO con información del inicio de sesión</returns>
        public AuthenticatedUserDTO BuildAuthenticatedUserObject(UsuarioDTO user, int? tiuiAmigoId, string sessionId = "")
        {
            AuthenticatedUserDTO authUser = new AuthenticatedUserDTO();
            authUser.User = user;
            authUser.AccessToken = BuildJwtToken(user);
            authUser.TiuiAmigoId = tiuiAmigoId;
            authUser.ExpireInSeconds = this._jwtSettings.TimeToExpiration * 60;
            authUser.SessionId = sessionId;
            return authUser;
        }
        /// <summary>
        /// Generar el token de acceso 
        /// </summary>
        /// <param name="appUser">Usuario con la información para el token</param>
        /// <returns>String con el token generado</returns>
        private string BuildJwtToken(UsuarioDTO appUser)
        {
            //Standard claims
            List<Claim> claims = GetClaims(appUser);
            //Creating jwt token                       
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["KEY_JWT_TIUI"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TimeToExpiration),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /// <summary>
        /// Obtiene los claims standar
        /// </summary>
        /// <param name="appUser">Usuario logeado</param>
        /// <returns>Listado de claims</returns>
        private static List<Claim> GetClaims(UsuarioDTO appUser)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name,appUser.NombreCompleto),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.NombreCompleto),
                new Claim(JwtRegisteredClaimNames.Email, appUser.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, appUser.UsuarioId.ToString()),
                new Claim(ClaimTypes.Role, appUser.TipoUsuario.ToString())
            };
        }

        public AuthenticatedAdminUserDTO BuildAuthenticatedAdminUserObject(UsuarioDTO user)
        {
            AuthenticatedAdminUserDTO authUser = new AuthenticatedAdminUserDTO();
            authUser.User = user;
            authUser.AccessToken = BuildJwtToken(user);
            authUser.ExpireInSeconds = this._jwtSettings.TimeToExpiration * 60;
            return authUser;
        }
    }
}
