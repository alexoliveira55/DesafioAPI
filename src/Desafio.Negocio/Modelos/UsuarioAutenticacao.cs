using System.ComponentModel.DataAnnotations;

namespace Desafio.Negocio.Modelos
{
    public class UsuarioAutenticacao : EntidadeBase
    {
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A {0} é obrigatória")]
        public string Senha { get; set; }
    }
}
