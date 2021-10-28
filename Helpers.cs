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
    public class Helpers
    {
        public static string ErrorOccured(string language)
        {
            if(language=="English") return "Error occured!\nPlease try again.";
            else if(language=="O'zbekcha") return "Xatolik yuz berdi!\nBoshqatdan urinib ko'ring.";
            else if(language=="Русский") return "Произошла ошибка!\nПожалуйста попробуйте ещё раз.";
            return "404";
        }
        public static string Welcome(string language)
        {
            if(language=="English") return "Welcome to our Prayer Time bot!\nIn order to get Namaz times please share your Location...";
            else if(language=="O'zbekcha") return "Prayer Time botga xush kelibsiz!\nNamoz vaqtini olish uchun iltimos manzilingizni ulashing...";
            return "Добро пожаловать в Prayer bot!\nДля того что бы узнать время молитвы отправьте вашу локацию...";
        }
        public static string LocationAccepted(string language)
        {
            if(language=="English") return ("Location accepted successfully\nNow you can get namaz time for your location...");
            else if(language=="O'zbekcha") return ("Manzil muvaffaqqiyatli qabul qilindi\nEndi siz namoz vaqtini manzilingiz uchun olishingiz mumkin... ");
            return ("Локация принято успешно\nТеперь вы можете получить время молитвы...");
        }
    }
}