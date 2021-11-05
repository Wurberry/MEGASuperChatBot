using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEGASuperChatBot
{
    public interface ICommandsRepository
    {
        public void SaveCommand(CommandEntity commandEntity);
        public CommandEntity GetCommandById(int id);
        public List<CommandEntity> GetAllCommands();
        public void UpdateCommandEntity(CommandEntity commandEntity);
        public void DeleteCommandEntity(int id);
        public List<CommandEntity> FindByCommandBySourceName(string sourceName);
        public List<CommandEntity> FindByCommandByAuthorName(string authorName);
        public List<CommandEntity> FindByCommandByTriggerName(string triggerName);


    }
}
