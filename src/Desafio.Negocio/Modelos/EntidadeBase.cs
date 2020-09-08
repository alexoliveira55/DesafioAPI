using System;

namespace Desafio.Negocio.Modelos
{
    public abstract class EntidadeBase
    {
        public EntidadeBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
