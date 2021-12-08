namespace MEGASuperChatBot
{
    public interface IChatService
    {
        public void ProcessMessage(ChatMessage chatMessage);
        public void RegisterBot(Bot bot);
    }
}