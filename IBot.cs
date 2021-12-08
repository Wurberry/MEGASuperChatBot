using System.Threading.Tasks;

namespace MEGASuperChatBot
{
    public interface IBot
    {
        Task SendMessageToChat(ChatMessage chatMessage);
    }
}
