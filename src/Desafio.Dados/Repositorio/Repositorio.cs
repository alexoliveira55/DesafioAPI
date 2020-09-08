using Desafio.Dados.Contexto;
using Desafio.Negocio.Interfaces;
using Desafio.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Desafio.Dados.Repositorio
{
    public abstract class Repositorio<TEntidadeBase> : IRepositorio<TEntidadeBase> where TEntidadeBase : EntidadeBase, new()
    {
        protected readonly ContextoDesafio _context;
        protected readonly List<TEntidadeBase> _Dados;
        public Repositorio(ContextoDesafio contextoDesafio)
        {
            _context = contextoDesafio;
            _Dados = _context.OtenhaInstanciaDados(typeof(TEntidadeBase)) as List<TEntidadeBase>;

        }
        public void Adicionar(TEntidadeBase entidade)
        {
            _Dados.Add(entidade);
        }

        public void Atualizar(TEntidadeBase entidade)
        {
            int index = _Dados.FindIndex(e => e.Id == entidade.Id);
            _Dados[index] = entidade;
        }

        public IEnumerable<TEntidadeBase> Buscar(Expression<Func<TEntidadeBase, bool>> consulta)
        {
            var resultado = _Dados.AsQueryable().Where(consulta).ToList();

            return resultado;
        }


        public TEntidadeBase ObterPorId(Guid Id)
        {
            return _Dados.Find(e => e.Id == Id);
        }

        public List<TEntidadeBase> ObterTodos()
        {
            return _Dados;
        }

        public void Remover(Guid Id)
        {
            int index = _Dados.FindIndex(e => e.Id == Id);
            _Dados.RemoveAt(index);
        }

    }
}
