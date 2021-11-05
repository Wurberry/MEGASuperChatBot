using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MEGASuperChatBot
{
    public class CommandRepository : ICommandsRepository
    {
        private readonly DatabaseContext _botDatabaseContext;
        public CommandRepository(DatabaseContext context)
        {
            _botDatabaseContext = context;
        }
        public void SaveCommand(CommandEntity commandEntity)
        {
            _botDatabaseContext.Add(commandEntity);
            _botDatabaseContext.SaveChanges();
        }
        public void UpdateCommandEntity(CommandEntity commandEntity)
        {
            _botDatabaseContext.Update(commandEntity);
            _botDatabaseContext.SaveChanges();
        }
        public CommandEntity GetCommandById(int id)
        {
            return _botDatabaseContext.Find<CommandEntity>(id);
        }
        public void DeleteCommandEntity(int id)
        {
            _botDatabaseContext.Remove(GetCommandById(id));
            _botDatabaseContext.SaveChanges();
        }
        public List<CommandEntity> GetAllCommands()
        {
            return _botDatabaseContext.CommandEntities.ToList();
        }
        public List<CommandEntity> FindByCommandBySourceName(string sourcename)
        {
            return _botDatabaseContext.CommandEntities
                .Where(entity => entity.SourcesNames.Contains(sourcename))
                .ToList();
        }
        public List<CommandEntity> FindByCommandByAuthorName(string authorname)
        {
            return _botDatabaseContext.CommandEntities
                .Where(entity => entity.CommandAuthor.Contains(authorname))
                .ToList();
        }
        public List<CommandEntity> FindByCommandByTriggerName(string triggername)
        {
            return _botDatabaseContext.CommandEntities
                .Where(entity => entity.CommandTrigger.Contains(triggername))
                .ToList();
        }
    }
}
