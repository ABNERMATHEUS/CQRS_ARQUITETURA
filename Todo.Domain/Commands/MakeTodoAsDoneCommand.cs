using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class MakeTodoAsDoneCommand : Notifiable, ICommand
    {
        public MakeTodoAsDoneCommand()
        {
        }

        public MakeTodoAsDoneCommand(Guid id, string user)
        {
            this.id = id;
            User = user;
        }

        public Guid id { get; set; }
        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(User, 6, "User", "Usuário inválido!")
                );
        }
    }
}
