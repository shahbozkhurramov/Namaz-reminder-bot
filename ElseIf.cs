using System;
using System.Threading;
using System.Threading.Tasks;
using bot.Entity;
using bot.HttpClients;
using bot.Services;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using bot.Dto.V2;
namespace bot
{
    public class ElseIf
    {
        public static async Task NullLocation(ITelegramBotClient client, Message message, string _language, IStorageService _storage,ICacheService _cache,float _longitude,float _latitude,ILogger<Handlers> _logger)
        {
            await _cache.GetOrUpdatePrayerTimeAsync(message.Chat.Id, message.Location.Longitude, message.Location.Latitude);
            await client.SendTextMessageAsync(
                message.Chat.Id,
                text: Helpers.LocationAccepted(_language),
                replyToMessageId: message.MessageId,
                replyMarkup: MessageBuilder.MenuShow(_language)
            );

            _longitude = message.Location.Longitude;
            _latitude = message.Location.Latitude;
            
            ChangeUser(client,  message,  _language,  _storage, _cache, _longitude, _latitude,_logger);
            Console.WriteLine($"{_latitude} {_longitude}");
        }
        public static async Task ChangeUser(ITelegramBotClient client, Message message, string _language, IStorageService _storage,ICacheService _cache,float _longitude,float _latitude,ILogger<Handlers> _logger)
        {
            var user1 = new BotUser(
            chatId: message.Chat.Id,
            username: message.From.Username,
            fullname: $"{message.From.FirstName} {message.From.LastName}",
            longitude: _longitude,
            latitude: _latitude,
            address: _language);

            var result=await _storage.InsertUserAsync(user1);
            
            if(result.IsSuccess)
            {
                _logger.LogInformation($"New user added: {message.Chat.Id}");
                await _storage.InsertUserAsync(user1);
            }
            else
            { 
                await _storage.UpdateUserAsync(user1);
                _logger.LogInformation($"User already exists!");
            }
        }

    }
}