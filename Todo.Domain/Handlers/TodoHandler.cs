using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsUndoneCommand>,
        IHandler<MakeTodoAsDoneCommand>
    {

        private readonly ITodoRepository _todoRepository;

        public TodoHandler(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult
                    (
                    false,
                    "Ops, parece que sua tarefa está errada!",
                    command.Notifications
                    );
            }

            //Gerar o TodoItem
            var todo = new TodoItem(command.Title, command.Date, command.User);


            //Salva no banco
            _todoRepository.Create(todo);

            //Retorno o resultado 
            return new GenericCommandResult(true, "Tarefa salva!", todo);

        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _todoRepository.GetById(command.Id, command.User);

            todo.UpdateTitle(command.Title);

            _todoRepository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);


        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _todoRepository.GetById(command.id, command.User);

            todo.MaskAsUndone();

            _todoRepository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MakeTodoAsDoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _todoRepository.GetById(command.id, command.User);

            todo.MarkAsDone();

            _todoRepository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}
