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
    public class Languages
    {
        public static string[,] Language = new string[3,8]{
                {"Ulashish", "Bugun", "Ertangi", 
                "Sozlamalar", "Tilni o'zgartirish", "Manzilni o'zgartirish", "Menyuga qaytish", "O'zbekcha"},
                
                {"Share", "Today", "Tomorrow", 
                "Settings", "Change Language", "Change Location", "Back to menu", "English"},

                {"Поделиться", "Сегодня", "Завтра", 
                "Настройки","Изменить язык",  "Изменить локацию", "В Меню","Русский"}
            };
        public static int CheckLanguage(string language)
        {
            if(language=="O'zbekcha")
            {
                return 0;
            }
            else if(language=="Русский")
            {
                return 2;
            }
            return 1;
        }
    }
}