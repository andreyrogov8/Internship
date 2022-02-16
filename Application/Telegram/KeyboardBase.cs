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
        public KeyboardBase(TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message; 
        }
        public static ReplyKeyboardMarkup BuildKeyboard(List<KeyboardButton> buttons, int numberOfColumns)
        {
            int counter = 0;
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (var button in buttons)
            {
                counter++;
                cols.Add(button);
                if (counter % numberOfColumns != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();

            }
            if (cols.Count > 0)
            {
                rows.Add(cols.ToArray());

            }
            var rmk = new ReplyKeyboardMarkup(rows);
            rmk.ResizeKeyboard = true;
            rmk.OneTimeKeyboard = true;

            return rmk;
        }
    }

}
