using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram
{
    public abstract class KeyboardBase
    {
        public TelegramBotClient _bot;
        public Message _message;
        public List<KeyboardButton[]> rows = new List<KeyboardButton[]>();
        public List<KeyboardButton> cols = new List<KeyboardButton>();
        public ReplyKeyboardMarkup _keyboard;
        public int counter = 0;
        public KeyboardBase(TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message; 
            _keyboard = new ReplyKeyboardMarkup(rows);
            _keyboard.ResizeKeyboard = true;
            _keyboard.OneTimeKeyboard = true;
        }
    }
}
