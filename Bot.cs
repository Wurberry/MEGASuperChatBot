using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace MEGASuperChatBot
{
    public class Bot : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly TelegramBotClient _botClient = new TelegramBotClient("2104392069:AAHPwTfZqb1LcPJb9gr9vbYXdTDtxQefj5E"); //Создание ТГ-бота - нужно указать токен _services это IServiceScopeFactory

        private readonly IServiceScopeFactory _services;

        private readonly ILogger<Bot> _logger;

        private ChatService chatServices;

        public Bot(IServiceScopeFactory services, ILogger<Bot> logger)
        {
            this._logger = logger;
            this._services = services;
        }

        public Bot(ILogger<Bot> logger)
        {
            this._logger = logger; // Забираем логгер с помощью ASP.NET Dependency Injection
        }

        public Task StartAsync(CancellationToken cancellationToken)
        { //Запускается при запуске IHostedService, при регистрации
            using var cts = new CancellationTokenSource();
            _botClient.StartReceiving(
            new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync), cts.Token); //Начинаем получать и обрабатывать сообщения/обновления в методах HandleUpdateAsync и HandleErrorAsync
            _logger.LogInformation("Bot init"); //Логируем

            var scope = _services.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
            var repository = new CommandRepository(dbContext);
            var chatService = new ChatService(repository); // Создаем сервис команд на основе репозитория
            chatService.RegisterBot(this);
            chatServices = chatService;

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public async Task SendMessageToChat(long chatId, String text)
        {// Метод для отправки сообщения в чат
            await _botClient.SendTextMessageAsync(
            chatId: chatId,
            text: text
            ); //Асинхронная отправка сообщения с укзанием ИД чата и самим текстом
        }
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        { //Поимка ошибок API и вывод их в лог
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error: \n[{ apiRequestException.ErrorCode}]\n{ apiRequestException.Message}",_ => exception.ToString() 
            }; // Распаковываем содержимое ошибки, получая сообщение ошибки
            
            _logger.LogError("Error on Telegram Api", exception); //Логируем ошибку
                
            return Task.CompletedTask; // Завершаем поток
        }
        
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
            { //Если полученное изменение в чате не сообщение - пропускаем
                return;
            }
            if (update.Message.Type != MessageType.Text)
            {//Если это не текст - пропускаем
                return;
            }

            ChatMessage chatMessage = new ChatMessage(update);
            chatServices.ProcessMessage(chatMessage);
            
            var chatId = chatMessage.getChatId(); //Получаем ИД чата
            _logger.LogInformation($"Получено '{chatMessage.getMessageText()}' в чате { chatId}."); // Для удобства логируем пришедшее сообщение
            //await SendMessageToChat(chatId, "Ответ"); //Отправляем ответ
        }
    }
}
