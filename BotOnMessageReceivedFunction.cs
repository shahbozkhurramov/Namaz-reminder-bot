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
        private string temp;
        private string _language="English";
        private async Task BotOnMessageReceived(ITelegramBotClient client, Message message)
        {
            Console.WriteLine($"@{message.From.Username} --> {message.From.FirstName} {message.From.LastName}");
            if(message.Location != null)
            {
                await _cache.GetOrUpdatePrayerTimeAsync(message.Chat.Id, message.Location.Longitude, message.Location.Latitude);
                await client.SendTextMessageAsync(
                    message.Chat.Id,
                    text: Function.LocationAccepted(_language),
                    replyToMessageId: message.MessageId,
                    replyMarkup: MessageBuilder.MenuShow(_language)
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
                    await _storage.UpdateUserAsync(user);
                    _logger.LogInformation($"User already exists!");
                }
                Console.WriteLine($"{_latitude} {_longitude}");
            }
            else
            { 
                Console.WriteLine($"{message.Text}");
                if(message.Text=="/start"){
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose Language\nTilni tanlang\nВыберите язык",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LanguageRequestButton());
                }
                else if((message.Text=="English" || message.Text=="O'zbekcha" || message.Text=="Русский") && temp=="/start"){
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Function.Welcome(_language),
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(message.Text)); 
                    _language=message.Text;
                }
                else if(message.Text=="English" || message.Text=="O'zbekcha" || message.Text=="Русский")
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(message.Text)); 
                    _language=message.Text;
                }
                else if(message.Text=="Change Location" || message.Text=="Manzilni o'zgartirish" || message.Text=="Изменить локацию") 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(_language));
                else if(message.Text=="Settings" || message.Text=="Sozlamalar" || message.Text=="Настройки") 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(_language));
                else if(message.Text=="Back to menu" || message.Text=="Menyuga qaytish" || message.Text=="В Меню")
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language));         
                else if((message.Text=="Cancel" || message.Text=="Orqaga" || message.Text=="Отмена") && (temp=="English" || temp=="O'zbekcha" || message.Text=="Русский"))
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language));
                else if(message.Text=="Cancel" || message.Text=="Orqaga" || message.Text=="Отмена") 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(_language));
                else if(message.Text=="Change Language" || message.Text=="Tilni o'zgartirish" || message.Text=="Изменить язык")
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LanguageRequestButton());
                }
                else if(message.Text=="Today" || message.Text=="Bugun" || message.Text=="Сегодня")
                {
                    Function.TimesWriter(client,message, _language, _storage, _cache);
                }
                else 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Function.ErrorOccured(_language));
                temp=message.Text;
            }
        }
    }
}