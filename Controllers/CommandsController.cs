using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MEGASuperChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly ICommandsRepository _commandRepository;
        public CommandsController(ICommandsRepository commandsRepository)
        {
            _commandRepository = commandsRepository;
        }
        
        [HttpGet]
        public IEnumerable<CommandEntity> Get()
        {
            return _commandRepository.GetAllCommands();
        }

        [HttpGet("find")]
        public IEnumerable<CommandEntity> Get(String sourceName)
        {
            return _commandRepository.FindByCommandBySourceName(sourceName);
        }

        [HttpGet("{id}")]
        public CommandEntity Get(int id)
        {
            return _commandRepository.GetCommandById(id);
        }

        [HttpPost]
        public void Post([FromBody]CommandEntity value)
        {
            _commandRepository.SaveCommand(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CommandEntity value)
        {
            value.CommandId = id;
            _commandRepository.UpdateCommandEntity(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _commandRepository.DeleteCommandEntity(id);
        }

    }
}
