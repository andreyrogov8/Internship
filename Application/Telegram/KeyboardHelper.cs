using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram
{
    public class KeyboardHelper
    {
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

            return rmk;
        }
    }
}
