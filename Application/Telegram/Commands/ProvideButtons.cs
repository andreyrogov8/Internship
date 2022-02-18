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



    public class ProvideButtons
    {
        public TelegramBotClient _bot;
        public Message _message;
        List<string> commandNames = new List<string>();
        public ProvideButtons(TelegramBotClient bot, Message message)
        {
            _bot = bot;
            _message = message;
        }
        public async Task Send()
        {
            commandNames.Add("Start");
            var buttons = new List<KeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new KeyboardButton($"{name}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 1);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "To start communication please press start button", replyMarkup: replyKeyboard);
        }
        public async Task Send(List<string> commandNames)
        {
            var buttons = new List<KeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new KeyboardButton($"{name}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            await _bot.SendTextMessageAsync(_message.Chat.Id, "Press Button", replyMarkup: replyKeyboard);
        }
    }
}
