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
        private string _language;
        private async Task BotOnMessageReceived(ITelegramBotClient client, Message message)
        {
            Console.WriteLine($"@{message.From.Username} --> {message.From.FirstName} {message.From.LastName}");
            if(message.Location!=null)
            {
                await ElseIf.NullLocation( client,  message,  _language,  _storage, _cache, _longitude, _latitude,_logger);
            }
            else
            {
                _language=Language.LanguageCheck(message.Text);
                Console.WriteLine($"{message.Text}");
                if(message.Text=="/start"){
                        await client.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose Language\nTilni tanlang\nВыберите язык",
                        parseMode: ParseMode.Markdown,
                        replyMarkup: MessageBuilder.LanguageRequestButton());
                        ElseIf.ChangeUser(client,  message,  _language,  _storage, _cache, _longitude, _latitude,_logger);
                }
                else if((message.Text=="English" || message.Text=="O'zbekcha" || message.Text=="Русский") && temp=="/start"){
                    _language=message.Text;
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Helpers.Welcome(_language),
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(message.Text)); 
                }
                else if(message.Text=="English" || message.Text=="O'zbekcha" || message.Text=="Русский")
                {
                    _language=message.Text;
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(message.Text)); 
                }
                else if(message.Text=="Change Location" || message.Text=="Manzilni o'zgartirish" || message.Text=="Изменить локацию") 
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(_language));
                }
                else if(message.Text=="Settings" || message.Text=="Sozlamalar" || message.Text=="Настройки") 
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(_language));
                }
                else if(message.Text=="Back to menu" || message.Text=="Menyuga qaytish" || message.Text=="В Меню")
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language)); 
                }        
                else if((message.Text=="Cancel" || message.Text=="Orqaga" || message.Text=="Отмена") && (temp=="English" || temp=="O'zbekcha" || temp=="Русский"))
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language));
                }
                else if(message.Text=="Cancel" || message.Text=="Orqaga" || message.Text=="Отмена") 
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text:message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(_language));
                }
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
                else if(message.Text=="Ertangi" || message.Text=="Tomorrow" || message.Text=="Завтра")
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Ertaga nima bo'lishini hech kim bilmidi...",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language));
                }
                else
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Helpers.ErrorOccured(_language));
                }
                temp=message.Text;
            }
        }
    }
}