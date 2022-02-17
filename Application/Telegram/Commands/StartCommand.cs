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
            commandNames.Add("/myBookings");
            var buttons = new List<KeyboardButton>();   
            foreach (var name in commandNames)
            {
                buttons.Add(new KeyboardButton(name));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Press Button", replyMarkup: replyKeyboard);
        }
    }
}