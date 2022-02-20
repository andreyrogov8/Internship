using Application.Telegram.Keyboards;
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
    public class StartCommand
    {
        public TelegramBotClient _bot;
        public Message _message;
        List<string> commandNames = new List<string>();
        public StartCommand(TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
        }

        public async Task Send()
        {
            commandNames.Add("getworkplaces");
            commandNames.Add("getbookings");
            commandNames.Add("/myBookings");
            var replyKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Press Button", replyMarkup: replyKeyboard);
        }
    }
}