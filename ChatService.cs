using System;
using System.Collections.Generic;

namespace MEGASuperChatBot
{
    public class ChatService : IChatService
    {
        private Bot _bot = null; //Ссылка на бота
        private readonly ICommandsRepository _commandRepoistory; //Класс-репозиторий с командами
        public ChatService(ICommandsRepository commandRepository)
        {
            this._commandRepoistory = commandRepository;
        }
        public void RegisterBot(Bot bot)
        {
            _bot = bot; //Регестрируем бота
        }
        async public void ProcessMessage(ChatMessage chatMessage)
        {
            if (_bot != null)
            {
                List<CommandEntity> commandTriggers = _commandRepoistory.FindByCommandByTriggerName(chatMessage.getMessageText());
                
                //Логика поиска и обработка комманд по имени триггера
                if (commandTriggers.Count != 0)
                {
                    foreach (var command in commandTriggers)
                    {
                        if(command.ScriptText != "")
                        {   // обработка триггеров, по ключевым словам в скрипт тексте
                            if(command.ScriptText == "Hello")
                            {
                                await _bot.SendMessageToChat(chatMessage.getChatId(), "Приветствую вас");
                            }
                        }
                        else 
                        {
                            await _bot.SendMessageToChat(chatMessage.getChatId(), command.CommandAnswer);
                        }
                    }
                }
                else // обработка команд, которые зависят от сообщения и не импользуют имя триггера
                {
                    if (chatMessage.getMessageText().Contains("^"))
                    {
                        await _bot.SendMessageToChat(chatMessage.getChatId(), "Ваше число: " + powBack(chatMessage.getMessageText()));
                    }
                }
                
            }
        }
        public double powBack(string msg)
        {
            double numb = 0;
            if (msg.Contains("^"))
            {
                string numbOS = msg.Substring(0, msg.IndexOf("^"));
                string numbST = msg.Substring(msg.IndexOf("^")+1);
                numb = Math.Pow(Convert.ToDouble(numbOS), Convert.ToDouble(numbST));
            }

            return numb;
        }
    }
}
