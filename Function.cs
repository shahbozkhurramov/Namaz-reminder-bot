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
    public class Function
    {
        public static async void TimesWriter(ITelegramBotClient client, Message message, string _language, IStorageService _storage, ICacheService _cache)
        {
            if(_language=="English" || _language=="O'zbekcha" || _language=="Русский")
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
                    replyMarkup: MessageBuilder.MenuShow(_language));
                    }
                    else{
                         await client.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "In order to get Namaz times please share your Location...",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: MessageBuilder.LocationRequestButton(_language));
                    }
            }
        }
    }
}