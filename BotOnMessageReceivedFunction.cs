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
        private static int lan;
        private string temp;
        private string _language;
        private async Task BotOnMessageReceived(ITelegramBotClient client, Message message)
        {
            Console.WriteLine($"@{message.From.Username} : {message.From.FirstName} {message.From.LastName}");
            if(message.Location!=null)
            {
                var user = await _storage.GetUserAsync(message.Chat.Id);
                _language=user.Address;
                await ElseIf.NullLocation(client,  message,  _language,  _storage, _cache,_logger);
            }
            else
            {
                var user = await _storage.GetUserAsync(message.Chat.Id);
                if(temp!="/start" && temp!="")
                    _language=user.Address;
            
                Console.WriteLine(message.Text+"-->"+_language);
                lan=Languages.CheckLanguage(_language);

                if((message.Text == "English" || message.Text == "O'zbekcha" || message.Text == "Русский") && temp == "/start")
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Helpers.Welcome(message.Text),
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(message.Text));
                    await ElseIf.NullLocation( client,  message,  message.Text,  _storage, _cache,_logger);
                }
                
                else if((message.Text=="English" || message.Text=="O'zbekcha" || message.Text=="Русский"))
                {
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(message.Text));
                    await ElseIf.NullLocation( client,  message,  message.Text,  _storage, _cache,_logger);
                }
                
                else if(message.Text=="/start")
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose Language\nTilni tanlang\nВыберите язык",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LanguageRequestButton());
                
                else if(message.Text==Languages.Language[lan,5]) 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(_language));
        
                else if(message.Text==Languages.Language[lan,3]) 
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.SettingsProperty(_language));
                
                else if(message.Text==Languages.Language[lan,6])
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.MenuShow(_language)); 
                
                else if(message.Text==Languages.Language[lan,4])
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: message.Text,
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LanguageRequestButton());
            
                else if(message.Text==Languages.Language[lan,1])
                {
                    Function.TimesWriter(client,message, _language, _storage, _cache);
                }
                
                else if(message.Text==Languages.Language[lan,2])
                {
                    Function.TimesWriterTomorrow(client,message, _language, _storage, _cache);
                }
                
                else
                    await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: Helpers.ErrorOccured(_language));
            
                temp=message.Text;
            }
        }
    }
}