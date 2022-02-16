using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Commands
{



    public class DefaultHandler : KeyboardBase
    {
        List<string> commandNames = new List<string>();
        public DefaultHandler(TelegramBotClient bot, Message message) : base(bot, message)
        {
    
        }
        public async Task Send()
        {
            commandNames.Add("Start");
            foreach (var name in commandNames)
            {
                counter++;
                cols.Add(new KeyboardButton($"{name}"));
                if (counter % 2 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }
            if (cols.Count > 0)
            {
                rows.Add(cols.ToArray());
            }
            await _bot.SendTextMessageAsync(_message.Chat.Id, "To start communication please press start button", replyMarkup: _keyboard);
        }
    }
}
