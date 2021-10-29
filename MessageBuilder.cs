using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot
{
    public class MessageBuilder
    {
        private static int lan;
        public static ReplyKeyboardMarkup LocationRequestButton(string language){
            lan=Languages.CheckLanguage(language);
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = Languages.Language[lan,0], RequestLocation = true }
                                }
                            },
                            ResizeKeyboard=true
            };
        }
        public static ReplyKeyboardMarkup MenuShow(string language){
            lan=Languages.CheckLanguage(language);
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = Languages.Language[lan,1]},
                                    new KeyboardButton(){ Text = Languages.Language[lan,2]},
                                    new KeyboardButton(){ Text = Languages.Language[lan,3]}
                                }
                            },
                            ResizeKeyboard=true
            };
        }
        public static ReplyKeyboardMarkup SettingsProperty(string language)
        {
            lan=Languages.CheckLanguage(language);
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = Languages.Language[lan,4] },
                                    new KeyboardButton(){ Text = Languages.Language[lan,5] },
                                    new KeyboardButton(){ Text = Languages.Language[lan,6] }
                                }
                            },
                            ResizeKeyboard=true
            };
        }
        public static ReplyKeyboardMarkup LanguageRequestButton()
            => new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "English" }, 
                                    new KeyboardButton(){ Text = "O'zbekcha" },
                                    new KeyboardButton(){ Text = "Русский" }

                                }
                            },
                            ResizeKeyboard=true
            };
    }
}