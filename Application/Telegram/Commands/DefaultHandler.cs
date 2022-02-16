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
            var buttons = new List<KeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new KeyboardButton($"{name}"));
            }
            var replyKeyboard = BuildKeyboard(buttons, 1);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "To start communication please press start button", replyMarkup: replyKeyboard);
        }
    }
}
