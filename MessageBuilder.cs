using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace bot
{
    public class MessageBuilder
    {
        public static ReplyKeyboardMarkup LocationRequestButton()
            => new ReplyKeyboardMarkup()
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
        public static ReplyKeyboardMarkup MenuShow()
            => new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "Today"},
                                    new KeyboardButton(){ Text = "Tomorrow"},
                                    new KeyboardButton(){ Text = "Weekly"},
                                    new KeyboardButton(){ Text = "Settings"}
                                }
                            },
                            ResizeKeyboard=true
            };
        public static ReplyKeyboardMarkup SettingsProperty()
            => new ReplyKeyboardMarkup()
            {
                Keyboard = new List<List<KeyboardButton>>()
                            {
                                new List<KeyboardButton>()
                                {
                                    new KeyboardButton(){ Text = "Change Location" },
                                    new KeyboardButton(){ Text = "Back to menu" }
                                }
                            },
                            ResizeKeyboard=true
            };
    }
}