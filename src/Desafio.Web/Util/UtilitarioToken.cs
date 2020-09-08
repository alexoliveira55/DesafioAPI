using Desafio.Negocio.Modelos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Desafio.Web.Util
{
    /// <summary>
    /// UtilitarioToken
    /// </summary>
    public static class UtilitarioToken
    {
        /// <summary>
        /// ChavePrivada
        /// </summary>
        public static string ChavePrivada 
        {
            get 
            {
                return "fedaf7d8863b48e197b9287d492b708e";
            } 
        }

        /// <summary>
        /// GerarToken
        /// </summary>
        /// <param name="usuarioAutenticacao"></param>
        /// <returns></returns>
        public static string GerarToken(UsuarioAutenticacao usuarioAutenticacao)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(ChavePrivada);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
             {
                 new Claim(ClaimTypes.Name, usuarioAutenticacao.Nome)
             }),
                Expires = DateTime.UtcNow.AddHours(2),
                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
