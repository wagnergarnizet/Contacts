using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Services
{
    public class TokenService : iTokenService

    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(Usuario usuario)
        {
            int usuarioExiste = ListaUsuario.Usuarios?.Any(u => u.Username == usuario.Username && u.Password == usuario.Password) ?? false ? 1 : 0;

            TipoPermissaoSistema permisao;

            if (usuarioExiste == 0)
            {
                return string.Empty;
            }
            else
            {
                var user = ListaUsuario.Usuarios?.FirstOrDefault(u => u.Username == usuario.Username && u.Password == usuario.Password);
                permisao = user?.PermissaoSistema ?? TipoPermissaoSistema.Admin;
            }
            var tokeHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT") ?? string.Empty);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, usuario.Username ?? string.Empty),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, permisao.ToString())

            }),
                //tempo de expiração do token
                Expires = DateTime.UtcNow.AddHours(1),

                //chave de criptografia
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            //cria o token
            var token = tokeHandler.CreateToken(tokenPropriedades);

            //retorna o token criado
            return tokeHandler.WriteToken(token);



        }
    }
}
