using Desafio.Negocio.Interfaces;
using Desafio.Negocio.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Desafio.Web.Controllers
{
    /// <summary>
    /// PessoaController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {

        private readonly IPessoaRepositorio _IPessoaRepositorio;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="iPessoaRepositorio"></param>
        public PessoaController(IPessoaRepositorio iPessoaRepositorio)
        {
            _IPessoaRepositorio = iPessoaRepositorio;
        }

        /// <summary>
        /// Rota para consulta de pessoas, que retorna uma lista de objeto de pessoas
        /// </summary>
        /// <returns>Lista de Todas as Pessoas</returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var resultado = _IPessoaRepositorio.ObterTodos();

                if (resultado == null) return NotFound(new { mensagem = "Nenhum registro encontrado" });

                if (resultado.Count() == 0) return NotFound(new { mensagem = "Nenhum registro encontrado" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }

        }

        /// <summary>
        /// Rota para consultar uma pessoa pelo seu código
        /// </summary>
        /// <param name="id">Id que deseja buscar</param>
        /// <returns>Obtejo pessoa</returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var resultado = _IPessoaRepositorio.ObterPorId(id);

                if (resultado == null)
                {
                    return NotFound(new { mensagem = string.Format("Id: {0} Inexistente", id) });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }
        }

        /// <summary>
        /// Rota para consultar pessoas de uma determinada UF
        /// </summary>
        /// <param name="Uf">Uf que deseja consultar</param>
        /// <returns>Lista de Pessoas da UF informada</returns>
        [Authorize]
        [HttpGet("uf={Uf}")]
        public IActionResult Get(string Uf)
        {
            try
            {
                var resultado = _IPessoaRepositorio.Buscar(p => p.Uf.ToUpper() == Uf.ToUpper());

                if (resultado == null) return NotFound(new { mensagem = string.Format("Nenhum registro encontrado com Uf: {0}", Uf) });

                if (resultado.Count() == 0) return NotFound(new { mensagem = string.Format("Nenhum registro encontrado com Uf: {0}", Uf) });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }
        }

        /// <summary>
        /// rota de gravar pessoa
        /// </summary>
        /// <param name="pessoa">Objeto pessoa preenchida. OBS.: não informar Id, pois é gerado automático</param>
        /// <returns>Objeto Pessoa</returns>
        [Authorize]
        [HttpPost("cadastrar")]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            try
            {
                if (!ValidaDadosPessoaInclusao(pessoa, out string mensagemRetorno))
                {
                    return BadRequest(new { mensagem = mensagemRetorno });
                }

                Pessoa novaPessoa = new Pessoa { Cpf = pessoa.Cpf, DataNascimento = pessoa.DataNascimento, Nome = pessoa.Nome, Uf = pessoa.Uf };

                _IPessoaRepositorio.Adicionar(novaPessoa);

                return Ok(novaPessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }

        }
        /// <summary>
        /// Rota para atualizar os dados de uma pessoa
        /// </summary>
        /// <param name="id">Id da pessoa que deseja alterar</param>
        /// <param name="pessoa">Objeto pessoa atualizado</param>
        /// <returns>Objeto pessoa com dados atualizados</returns>
        [Authorize]
        [HttpPut("alterar/{id}")]
        public IActionResult Put(Guid id, [FromBody] Pessoa pessoa)
        {
            try
            {
                var pessoaAlterar = _IPessoaRepositorio.ObterPorId(id);

                if (pessoaAlterar == null)
                {
                    return NotFound(new { mensagem = string.Format("Id: {0} Inexistente", id) });
                }

                if (!ValidaDadosPessoaAlteracao(id, pessoa, out string mensagemRetorno))
                {
                    return BadRequest(new { mensagem = mensagemRetorno });
                }

                pessoa.Id = id;

                _IPessoaRepositorio.Atualizar(pessoa);

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }
        }

        /// <summary>
        /// Rota para excluir uma pessoa
        /// </summary>
        /// <param name="id">Id da pessoa que deseja excluir</param>
        /// <returns>mensagem</returns>
        [Authorize]
        [HttpDelete("excluir/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var pessoaExcluir = _IPessoaRepositorio.ObterPorId(id);

                if (pessoaExcluir == null)
                {
                    return NotFound(new { mensagem = string.Format("Id: {0} Inexistente", id) });
                }

                _IPessoaRepositorio.Remover(id);

                return Ok(new { message = string.Format("{0} Excluído com sucesso!", pessoaExcluir.Nome) });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = "ERRO Inesperado. " + ex.Message });
            }
        }
        /// <summary>
        /// Valida dados do Objeto Pessoa para inclusão
        /// </summary>
        /// <param name="pessoa">Objeto para validação</param>
        /// <param name="mensagem">Mensagem de retorno</param>
        /// <returns>verdadeiro ou falso</returns>
        private bool ValidaDadosPessoaInclusao(Pessoa pessoa, out string mensagem)
        {
            mensagem = string.Empty;
            if (pessoa == null)
            {
                mensagem = "O objeto pessoa está nulo";
                return false;
            }

            var resultadoBusca = _IPessoaRepositorio.Buscar(p => p.Nome == pessoa.Nome && p.Cpf == pessoa.Cpf);

            if (resultadoBusca.Count() > 0)
            {
                mensagem = $"A pessoa {pessoa.Nome} já existe.";
                return false;
            }
            resultadoBusca = null;
            resultadoBusca = _IPessoaRepositorio.Buscar(p => p.Cpf == pessoa.Cpf);

            if (resultadoBusca.Count() > 0)
            {
                mensagem = $"Já existe outra pessoa com este CPF {pessoa.Cpf}!";
                return false;
            }

            if (pessoa.DataNascimento > DateTime.Now)
            {
                mensagem = "A data de nascimento não pode ser maior que a Data de Hoje!";
                return false;
            }

            return true;
        }
        /// <summary>
        /// Valida dados do Objeto Pessoa para inclusão
        /// </summary>
        /// <param name="Id">Id da Pessoa a ser alterada</param>
        /// <param name="pessoa">Objeto para validação</param>
        /// <param name="mensagem">Mensagem de retorno</param>
        /// <returns>verdadeiro ou falso</returns>
        private bool ValidaDadosPessoaAlteracao(Guid Id, Pessoa pessoa, out string mensagem)
        {
            mensagem = string.Empty;
            if (pessoa == null)
            {
                mensagem = "O objeto pessoa está nulo";
                return false;
            }

            var resultadoBusca = _IPessoaRepositorio.Buscar(p => p.Nome == pessoa.Nome && p.Cpf == pessoa.Cpf && p.Id != Id);

            if (resultadoBusca.Count() > 0)
            {
                mensagem = "Já existe outra pessoa com mesmo nome e CPF informados.";
                return false;
            }
            
            resultadoBusca = null;
            resultadoBusca = _IPessoaRepositorio.Buscar(p => p.Cpf == pessoa.Cpf && p.Id != Id);

            if (resultadoBusca.Count() > 0)
            {
                mensagem = $"Já existe outra pessoa com este CPF {pessoa.Cpf}!";
                return false;
            }

            if (pessoa.DataNascimento > DateTime.Now)
            {
                mensagem = "A data de nascimento não pode ser maior que a Data de Hoje!";
                return false;
            }

            return true;
        }
    }
}
