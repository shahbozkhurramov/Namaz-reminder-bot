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
    public partial class Handlers
    {
         private async Task BotOnMessageReceived(ITelegramBotClient client, Message message)
        {
            if(message.Location != null)
            {
                await _cache.GetOrUpdatePrayerTimeAsync(message.Chat.Id, message.Location.Longitude, message.Location.Latitude);
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    "Location successfully accepted\nNow you can get your timezone prayertime",
                    replyToMessageId: message.MessageId,
                    replyMarkup: MessageBuilder.MenuShow()
                );
                _longitude = message.Location.Longitude;
                _latitude = message.Location.Latitude;
                var user = new BotUser(
                    chatId: message.Chat.Id,
                    username: message.From.Username,
                    fullname: $"{message.From.FirstName} {message.From.LastName}",
                    longitude: _longitude,
                    latitude: _latitude,
                    address: string.Empty);
                var result=await _storage.InsertUserAsync(user);
                if(result.IsSuccess)
                {
                    _logger.LogInformation($"New user added: {message.Chat.Id}");
                    await _storage.InsertUserAsync(user);
                }
                else{ 
                    _logger.LogInformation($"User already exists!");
                    await _storage.UpdateUserAsync(user);
                }
                Console.WriteLine($"{_latitude} {_longitude}");
                Console.WriteLine($"@{message.From.Username} --> {message.From.FirstName} {message.From.LastName}");
            }
            else
            { 
                Console.WriteLine($"{message.Text}");
                if(message.Text=="/start"){
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Welcome to our Prayer Time bot\nIn order to get Namaz times please share your Location...",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton());
                    Console.WriteLine($"@{message.From.Username} --> {message.From.FirstName} {message.From.LastName}");}
                else if(message.Text=="Change Location") await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Change Location",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton());
                else if(message.Text=="Settings") await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Settings",
                            parseMode: ParseMode.Markdown,
                            replyMarkup: MessageBuilder.SettingsProperty());
                else if(message.Text=="Back to menu")
                            await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Menu",
                            parseMode: ParseMode.Markdown,
                            replyMarkup: MessageBuilder.MenuShow());         
                else if(message.Text=="Cancel")
                            await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text:"Cancel",
                            parseMode: ParseMode.Markdown,
                            replyMarkup: MessageBuilder.MenuShow());
                else if(message.Text=="Today")
                {
                    if(await _storage.ExistsAsync(message.Chat.Id))
                    {
                        var res = await _storage.GetUserAsync(message.Chat.Id);
                        var result = await _cache.GetOrUpdatePrayerTimeAsync(res.ChatId, res.Longitude, res.Latitude);
                        var times = result.prayerTime;
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: @$"
*Fajr*: {times.Fajr}
*Sunrise*: {times.Sunrise}
*Dhuhr*: {times.Dhuhr}
*Asr*: {times.Asr}
*Maghrib*: {times.Maghrib}
*Isha*: {times.Isha}
*Midnight*: {times.Midnight}
                    
*Method*: {times.CalculationMethod}
                    ",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow());
                    }
                    else{
                         await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "In order to get Namaz times please share your Location...",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton());
                    }

                }
                else await client.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Error occured!\nPlease try again.");
            }
        }
    }
}