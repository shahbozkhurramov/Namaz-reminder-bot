using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot
{
    public class MessageBuilder
    {
        public static ReplyKeyboardMarkup LocationRequestButton(string language){
            if(language=="O'zbekcha")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Ulashish", RequestLocation = true },
                                        new KeyboardButton(){ Text = "Orqaga" } 
                                    }
                                },
                                ResizeKeyboard=true
                };
            }
            else if(language=="Русский")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Поделиться", RequestLocation = true },
                                        new KeyboardButton(){ Text = "Отмена" } 
                                    }
                                },
                                ResizeKeyboard=true
                };
            }
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "Share", RequestLocation = true },
                                    new KeyboardButton(){ Text = "Cancel" } 
                                }
                            },
                            ResizeKeyboard=true
            };
        }
        public static ReplyKeyboardMarkup MenuShow(string language){
            if(language=="O'zbekcha")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Bugun"},
                                        new KeyboardButton(){ Text = "Ertangi"},
                                        new KeyboardButton(){ Text = "Sozlamalar"}
                                    }
                                },
                                ResizeKeyboard=true
                };
            }
            else if(language=="Русский")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Сегодня"},
                                        new KeyboardButton(){ Text = "Завтра"},
                                        new KeyboardButton(){ Text = "Настройки"}
                                    }
                                },
                                ResizeKeyboard=true
                };
            }
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "Today"},
                                    new KeyboardButton(){ Text = "Tomorrow"},
                                    new KeyboardButton(){ Text = "Settings"}
                                }
                            },
                            ResizeKeyboard=true
            };
        }
        public static ReplyKeyboardMarkup SettingsProperty(string language)
        {
            if(language=="O'zbekcha")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Tilni o'zgartirish" },
                                        new KeyboardButton(){ Text = "Manzilni o'zgartirish" },
                                        new KeyboardButton(){ Text = "Menyuga qaytish" }
                                    }
                                },
                                ResizeKeyboard=true
                }; 
            }
            else if(language=="Русский")
            {
                return new ReplyKeyboardMarkup()
                {
                    Keyboard = new List<List<KeyboardButton>>()
                                {
                                    new List<KeyboardButton>()
                                    {
                                        new KeyboardButton(){ Text = "Изменить язык" },
                                        new KeyboardButton(){ Text = "Изменить локацию" },
                                        new KeyboardButton(){ Text = "В Меню" }
                                    }
                                },
                                ResizeKeyboard=true
                };
            }
            return new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "Change Language" },
                                    new KeyboardButton(){ Text = "Change Location" },
                                    new KeyboardButton(){ Text = "Back to menu" }
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