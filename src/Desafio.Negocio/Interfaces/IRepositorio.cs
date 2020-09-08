using Desafio.Negocio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Desafio.Negocio.Interfaces
{
    public interface IRepositorio<TEntidadeBase> where TEntidadeBase: EntidadeBase
    {
        void Adicionar(TEntidadeBase entidade);
        void Atualizar(TEntidadeBase entidade);
        void Remover(Guid Id);
        TEntidadeBase ObterPorId(Guid Id);
        List<TEntidadeBase> ObterTodos();
        IEnumerable<TEntidadeBase> Buscar(Expression<Func<TEntidadeBase, bool>> consulta);
    }
}
