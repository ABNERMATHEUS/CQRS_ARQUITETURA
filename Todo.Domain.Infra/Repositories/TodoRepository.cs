using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Domain.Entities;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly DataContext.Context _dataContext;

        public TodoRepository(DataContext.Context dataContext)
        {
            _dataContext = dataContext;
        }

        public void Create(TodoItem todo)
        {
            _dataContext.Todos.Add(todo);
            _dataContext.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _dataContext.Todos.AsNoTracking().Where(TodoQueries.GetAll(user)).OrderBy(x=>x.Date);
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
            return _dataContext.Todos.AsNoTracking().Where(TodoQueries.GetAllDone(user)).OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllUndone(string user)
        {
            return _dataContext.Todos.AsNoTracking().Where(TodoQueries.GetAllUndone(user)).OrderBy(x => x.Date);
        }

        public TodoItem GetById(Guid id, string user)
        {
            return _dataContext.Todos.FirstOrDefault(TodoQueries.GetById(id,user)); //Se eu usar só o First e não tiver o dado, ele vai dar um exception
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _dataContext.Todos.AsNoTracking().Where(TodoQueries.GetByPeriod(user, date, done)).OrderBy(x => x.Date);
        }

        public void Update(TodoItem todo)
        {
            _dataContext.Entry(todo).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
    }
}
