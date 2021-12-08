using Telegram.Bot.Types;

namespace MEGASuperChatBot
{
    public class ChatMessage
    {
        private long chatId;
        private string messageText;

        public ChatMessage()
        {

        }

        public ChatMessage(Update update)
        {
            chatId = update.Message.Chat.Id;
            messageText = update.Message.Text;
        }

        public long getChatId() { return this.chatId; }
        public string getMessageText() { return this.messageText; }

    }
}
