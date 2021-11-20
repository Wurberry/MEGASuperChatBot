using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MEGASuperChatBot
{
    public class Bot : Microsoft.Extensions.Hosting.IHostedService
    {
        private readonly ILogger<Bot> _logger;//Логгер для красивого логирования

        private readonly TelegramBotClient _botClient = new TelegramBotClient("2104392069:AAHPwTfZqb1LcPJb9gr9vbYXdTDtxQefj5E"); //Создание ТГ-бота - нужно указать токен
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
            
            var chatId = update.Message.Chat.Id; //Получаем ИД чата
            _logger.LogInformation($"Получено '{update.Message.Text}' в чате { chatId}."); // Для удобства логируем пришедшее сообщение
            await SendMessageToChat(chatId, "Ответ"); //Отправляем ответ
        }
    }
}
