using Desafio.Dados.Contexto;
using Desafio.Negocio.Interfaces;
using Desafio.Negocio.Modelos;

namespace Desafio.Dados.Repositorio
{
    public class UsuarioAutenticacaoRepositorio : Repositorio<UsuarioAutenticacao>, IUsuarioAutenticacaoRepositorio
    {
        public UsuarioAutenticacaoRepositorio(ContextoDesafio contexto) : base(contexto) { }

        public UsuarioAutenticacao ObterUsuarioPorNomeESenha(string nome, string senha)
        {
            return _Dados.Find(u => u.Nome == nome && u.Senha == senha);
        }
    }
}
