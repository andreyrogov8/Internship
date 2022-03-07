using Application.Telegram.Keyboards;
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
        List<string> commandNames = new List<string>();
        public ProvideButtons(TelegramBotClient bot)
        {
            _bot = bot;
        }

        public async Task SendAsync(CallbackQuery callbackQuery,List<string> commandNames,string textToShow, int numberOfColumns, string commandCall)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns, commandCall);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                callbackQuery.Data.Contains("BACK") ? "Press Button": textToShow 
                , replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }

        public async Task SendAsync(Message message, List<string> commandNames, int numberOfColumns)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns);
            var currentMessage = await _bot.SendTextMessageAsync(message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(message.From.Id, currentMessage.MessageId);
        }
    }
}
