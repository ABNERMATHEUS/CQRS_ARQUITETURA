using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{

    [TestClass]
    public class TodoQueriesTests
    {
        private List<TodoItem> _items;

        public TodoQueriesTests()
        {
            _items = new List<TodoItem>();
            var todo = new TodoItem("Tarefa 3", DateTime.Now, "abner_math");
            todo.MarkAsDone();
            _items.Add(new TodoItem("Tarefa 1", DateTime.Now, "usuarioA"));
            _items.Add(new TodoItem("Tarefa 2", DateTime.Now, "usuarioA"));
            _items.Add(todo);
            _items.Add(new TodoItem("Tarefa 4", DateTime.Now, "usuarioA"));
            _items.Add(new TodoItem("Tarefa 5", DateTime.Now, "abner_math"));
        }

        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_abner_math()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("abner_math")); //AsQueryable() é utilizado quando colocamos expressões dentro do Where, por exemplo
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void Data_a_consulta_deve_retornar_1_tarefa_apenas_do_usuario_abner_math()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("abner_math"));
            Assert.AreEqual(1, result.Count());
        }
    }
}
