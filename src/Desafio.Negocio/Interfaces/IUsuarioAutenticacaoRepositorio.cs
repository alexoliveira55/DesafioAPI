using Desafio.Negocio.Modelos;

namespace Desafio.Negocio.Interfaces
{
    public interface IUsuarioAutenticacaoRepositorio: IRepositorio<UsuarioAutenticacao>
    {

        UsuarioAutenticacao ObterUsuarioPorNomeESenha(string nome, string senha);

    }
}
