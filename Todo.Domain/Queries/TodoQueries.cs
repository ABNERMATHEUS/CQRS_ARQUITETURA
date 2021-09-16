using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class TodoQueries
    {
        public static Expression<Func<TodoItem,bool>>GetAll(string user)
        {
            return x => x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetAllDone(string user)
        {
            return x => x.User == user && x.Done == true;
        }

        public static Expression<Func<TodoItem,bool>> GetAllUndone(string user)
        {
            return x => x.User == user && x.Done == false;
        }

        public static Expression<Func<TodoItem,bool>>GetByPeriod(string user, DateTime date, bool done)
        {
            return x =>
            x.User == user &&
            x.Done == done &&
            x.Date.Date == date.Date;  //date.Date -> ele pega só a data e ignora a hora, quando colocamos mais um Date
        }

        public static Expression<Func<TodoItem,bool>>GetById(Guid id,string user)
        {
            return x => x.Id == id && x.User == user;
        }

    }
}
