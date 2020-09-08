using Desafio.Negocio.Interfaces;
using Desafio.Negocio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Desafio.Web.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUsuarioAutenticacaoRepositorio _IUsuarioAutenticacaoRepositorio;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="iUsuarioAutenticacaoRepositorio"></param>
        public HomeController(IUsuarioAutenticacaoRepositorio iUsuarioAutenticacaoRepositorio)
        {
            _IUsuarioAutenticacaoRepositorio = iUsuarioAutenticacaoRepositorio;
        }


        /// <summary>
        /// Rota para autenticação. 
        /// </summary>
        /// <param name="usuarioAutenticacao"> Objeto de UsuarioAutenticacao. Informar somente Nome e Senha => Nome=globaltec | Senha=globaltec</param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public IActionResult Autenticar([FromBody] UsuarioAutenticacao usuarioAutenticacao)
        {
            try
            {
                var usuario = _IUsuarioAutenticacaoRepositorio.ObterUsuarioPorNomeESenha(usuarioAutenticacao.Nome, usuarioAutenticacao.Senha);

                if (usuario == null)
                    return NotFound(new { message = "Usuário ou senha inválidos" });

                var tokenGerado = Util.UtilitarioToken.GerarToken(usuario);

                usuario.Senha = "";

                JsonResult Json = new JsonResult(new { user = usuario, token = tokenGerado });

                return Json;
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }
        }
    }
}
