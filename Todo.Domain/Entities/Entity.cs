using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>//IEquatable<Entity> - interface para fazer comporações entre um ou mais objetos do mesmo tipo
    {
        public Guid Id { get; private set; }  //Private - SOLID 

        protected Entity()//protect os filhos tem acesso
        {
            Id = Guid.NewGuid();
        }

        //override da IEquatable<Entity>
        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}
