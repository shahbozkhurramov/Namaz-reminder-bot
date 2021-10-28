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
using System.Collections;
using System.Collections.Generic;
namespace bot
{
    public class Language
    {
        private static List<string> Uzbek=new List<string>(){"Ulashish", "Orqaga", "Bugun", "Ertangi", 
        "Sozlamalar", "Tilni o'zgartirish", "Manzilni o'zgartirish", "Menyuga qaytish", "O'zbekcha"};
        private static List<string> English=new List<string>(){"Share", "Cancel", "Today", "Tomorrow", 
        "Settings", "Changa Language", "Change Location", "Back to menu", "English"};
        private static List<string> Russian=new List<string>(){"Изменить язык", "Поделиться", "Отмена", "Сегодня", "Завтра", 
        "Настройки", "Изменить локацию", "В Меню","Русский"};
        public static string LanguageCheck(string message)
        {
            if(English.Contains(message))
            {
                return "English";
            }
            else if(Uzbek.Contains(message))
            {
                return "O'zbekcha";
            }
            else if(Russian.Contains(message))
            { 
                return "Русский";
            }
            else return "error";
        }
    }
}