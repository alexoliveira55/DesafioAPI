using Desafio.Negocio.Modelos;
using System;
using System.Collections.Generic;

namespace Desafio.Dados.Contexto
{
    public class ContextoDesafio
    {
        private static List<UsuarioAutenticacao> _UsuariosAutenticacao = null;
        private static List<Pessoa> _Pessoas = null;
        public List<UsuarioAutenticacao> UsuariosAutenticacao
        {
            get
            {
                if (_UsuariosAutenticacao == null)
                {
                    _UsuariosAutenticacao = new List<UsuarioAutenticacao>
                    {
                        new UsuarioAutenticacao { Nome = "alex", Senha = "alex" },
                        new UsuarioAutenticacao { Nome = "globaltec", Senha = "globaltec" }
                    };
                }

                return _UsuariosAutenticacao;
            }
        }

        public List<Pessoa> Pessoas
        {
            get
            {
                if(_Pessoas == null) 
                {
                    _Pessoas = new List<Pessoa>
                    {
                        new Pessoa { Nome = "Alex de Oliveira", Cpf = "719.456.789-89", DataNascimento = new DateTime(1990, 1, 18), Uf = "GO" },
                        new Pessoa { Nome = "João de Oliveira", Cpf = "729.456.789-89", DataNascimento = new DateTime(1983, 1, 17), Uf = "GO" },
                        new Pessoa { Nome = "Maria de Oliveira", Cpf = "739.456.789-89", DataNascimento = new DateTime(1984, 3, 16), Uf = "GO" },
                        new Pessoa { Nome = "Joana de Oliveira", Cpf = "749.456.789-89", DataNascimento = new DateTime(1985, 4, 15), Uf = "GO" },
                        new Pessoa { Nome = "Suzana de Oliveira", Cpf = "759.456.789-89", DataNascimento = new DateTime(1986, 5, 14), Uf = "GO" },
                        new Pessoa { Nome = "José de Oliveira", Cpf = "769.456.789-89", DataNascimento = new DateTime(1987, 6, 13), Uf = "BA" },
                        new Pessoa { Nome = "Ana de Oliveira", Cpf = "779.456.789-89", DataNascimento = new DateTime(1988, 7, 12), Uf = "BA" },
                        new Pessoa { Nome = "Miguel de Oliveira", Cpf = "789.456.789-89", DataNascimento = new DateTime(1989, 8, 11), Uf = "PE" },
                        new Pessoa { Nome = "Sara de Oliveira", Cpf = "799.456.789-89", DataNascimento = new DateTime(1990, 9, 10), Uf = "SP" },
                        new Pessoa { Nome = "Silvio de Oliveira", Cpf = "710.456.789-89", DataNascimento = new DateTime(1991, 10, 9), Uf = "SP" },
                        new Pessoa { Nome = "Heloanny de Oliveira", Cpf = "711.456.789-89", DataNascimento = new DateTime(1992, 11, 8), Uf = "RJ" },
                        new Pessoa { Nome = "Sandra de Oliveira", Cpf = "712.456.789-89", DataNascimento = new DateTime(2000, 12, 7), Uf = "RJ" },
                        new Pessoa { Nome = "Wilson de Oliveira", Cpf = "713.456.789-89", DataNascimento = new DateTime(2001, 11, 6), Uf = "MG" },
                        new Pessoa { Nome = "Juliana de Oliveira", Cpf = "714.456.789-89", DataNascimento = new DateTime(2010, 10, 5), Uf = "MG" },
                        new Pessoa { Nome = "Giovana de Oliveira", Cpf = "715.456.789-89", DataNascimento = new DateTime(1998, 9, 4), Uf = "PA" },
                        new Pessoa { Nome = "Lucas de Oliveira", Cpf = "716.456.789-89", DataNascimento = new DateTime(1994, 8, 3), Uf = "PA" },
                        new Pessoa { Nome = "Leandro de Oliveira", Cpf = "717.456.789-89", DataNascimento = new DateTime(1996, 7, 2), Uf = "MA" },
                        new Pessoa { Nome = "Caua de Oliveira", Cpf = "718.456.789-89", DataNascimento = new DateTime(1997, 6, 1), Uf = "MA" }
                    };

                }

                return _Pessoas;
            }
        }

        internal IEnumerable<object> OtenhaInstanciaDados(Type type)
        {
            return type.Name switch
            {
                "UsuarioAutenticacao" => UsuariosAutenticacao,
                "Pessoa" => Pessoas,
                _ => null,
            };
        }

    }
}
