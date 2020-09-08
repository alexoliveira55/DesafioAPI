using Desafio.Dados.Contexto;
using Desafio.Negocio.Interfaces;
using Desafio.Negocio.Modelos;

namespace Desafio.Dados.Repositorio
{
    public class PessoaRepositorio : Repositorio<Pessoa>, IPessoaRepositorio
    {
        public PessoaRepositorio(ContextoDesafio contexto) : base(contexto){}
    }
}
