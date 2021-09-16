using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {
        private readonly string _id_user = "bab64c511fd4904466edfbd2a75dff8a";

        #region GET
        [HttpGet]
        public IEnumerable<TodoItem> GetAll([FromServices] ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAll(user);
        }


        [Route("done")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDone([FromServices] ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllDone(user);
        }

        [Route("undone")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndone([FromServices]ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetAllUndone(user);
        }

        [Route("done/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForToday([FromServices] ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date, true);
        }

        [Route("undone/today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetInactiveForToday([FromServices] ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date, false);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForTomorrow([FromServices] ITodoRepository repository)
        {
            var user = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(user, DateTime.Now.Date.AddDays(1), true);
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForTomorrow([FromServices] ITodoRepository repository)
        {
            var user = _id_user;//User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return repository.GetByPeriod(
                user,
                DateTime.Now.Date.AddDays(1),
                false
            );
        }

        #endregion GET

        #region POST
        [HttpPost]        
        public GenericCommandResult Create([FromBody] CreateTodoCommand command, [FromServices] TodoHandler handler)
        {
            command.User = _id_user;//User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)handler.Handle(command);
        }
        #endregion POST

        #region PUT

        [HttpPut]
        public GenericCommandResult Update([FromBody] UpdateTodoCommand command, [FromServices] TodoHandler handler)
        {
            command.User = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value; 
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-done")]
        [HttpPut]
        public GenericCommandResult MarkAsDone([FromBody] MakeTodoAsDoneCommand command, [FromServices] TodoHandler handler)
        {
            command.User = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value; 
            return (GenericCommandResult)handler.Handle(command);
        }

        [Route("mark-as-undone")]
        [HttpPut]
        public GenericCommandResult MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command, [FromServices] TodoHandler handler)
        {
            command.User = _id_user; //User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value; 
            return (GenericCommandResult)handler.Handle(command);
        }

        #endregion PUT

    }

}
