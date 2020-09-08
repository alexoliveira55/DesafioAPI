using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Negocio.Modelos
{
    public class Pessoa : EntidadeBase
    {
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(100, ErrorMessage = "{0} deve ter entre {2} e {1} carcteres", MinimumLength = 5)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(14, ErrorMessage = "{0} deve ter entre {2} e {1} carcteres", MinimumLength = 11)]
        [RegularExpression(@"^((\d{3}).(\d{3}).(\d{3})-(\d{2}))*$", ErrorMessage = "{0} com formato inválido. Informe Utilize o formato (000.000.000-00)")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(2, ErrorMessage = "{0} deve conter apenas {1} carcteres", MinimumLength = 2)]
        public string Uf { get; set; }
        [Required(ErrorMessage = "{0} é obrigatório")]
        public DateTime DataNascimento { get; set; }

    }
}
