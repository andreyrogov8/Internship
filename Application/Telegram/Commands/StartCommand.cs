using MediatR;
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
    public class StartCommand : KeyboardBase
    {
        List<string> commandNames = new List<string>();
        public StartCommand(TelegramBotClient bot, Message message) : base (bot, message)
        {

        }
        public async Task Send()
        {
            commandNames.Add("getworkplaces");
            commandNames.Add("getbookings");
            foreach (var name in commandNames)
            {
                counter++;
                cols.Add(new KeyboardButton($"{name}"));
                if (counter % 2 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Press Button", replyMarkup: _keyboard);
        }
    }
}