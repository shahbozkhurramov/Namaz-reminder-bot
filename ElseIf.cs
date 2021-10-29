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
        public static async Task NullLocation(ITelegramBotClient client, Message message, string _language, IStorageService _storage,ICacheService _cache,ILogger<Handlers> _logger)
        {
            var user = await _storage.GetUserAsync(message.Chat.Id);
            user.Address =_language;
            await _storage.UpdateUserAsync(user);
            await _cache.GetOrUpdatePrayerTimeAsync(message.Chat.Id, message.Location.Longitude, message.Location.Latitude);
            await client.SendTextMessageAsync(
                message.Chat.Id,
                text: Helpers.LocationAccepted(user.Address),
                replyToMessageId: message.MessageId,
                replyMarkup: MessageBuilder.MenuShow(user.Address)
            );

            float _longitude = message.Location.Longitude;
            float _latitude = message.Location.Latitude;
            if(user is null)
            {
                user = new BotUser(
                    chatId: message.Chat.Id,
                    username: message.From.Username,
                    fullname: $"{message.From.FirstName} {message.From.LastName}",
                    longitude: _longitude,
                    latitude: _latitude,
                    address: _language);
                
                await _storage.InsertUserAsync(user);
                _logger.LogInformation($"New user added: {message.Chat.Id}");
            }
            else
            {
                user.Longitude = _longitude;
                user.Latitude = _latitude;
                user.Address =_language;
                await _storage.UpdateUserAsync(user);
                _logger.LogInformation($"User {user.ChatId} updated!");
            }
        }
    }
}