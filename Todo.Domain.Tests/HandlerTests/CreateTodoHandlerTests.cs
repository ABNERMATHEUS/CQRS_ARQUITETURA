using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", DateTime.Now, "");
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Título tarefa", DateTime.Now, "abner_math");
        private readonly TodoHandler _todoHandler = new TodoHandler(new FakeTodoRepository());
        private GenericCommandResult _result;
        public void Dado_um_comando_invalido_deve_interromper_a_execucao()
        {
                       
            _result = (GenericCommandResult)_todoHandler.Handle(_invalidCommand);
            Assert.AreEqual(_result.Success,false);
        }

        public void Dado_um_comando_valido_deve_criar_a_tarefa()
        {
            
            var _result = (GenericCommandResult)_todoHandler.Handle(_validCommand);
            Assert.AreEqual(_result.Success,true);
        }
    }
}
