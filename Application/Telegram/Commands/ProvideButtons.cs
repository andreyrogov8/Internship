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

        public async Task Send(CallbackQuery callbackQuery,List<string> commandNames, int numberOfColumns)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns);
            var currentMessage = await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id,
                callbackQuery.Data.Contains("BACK") ? "Press Button": $"You clicked: {callbackQuery.Data} \n Press Button"
                , replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
        public async Task Send(Message message, List<string> commandNames, int numberOfColumns)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns);
            var currentMessage = await _bot.SendTextMessageAsync(message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
            UserStateStorage.AddMessage(message.From.Id, currentMessage.MessageId);
        }
    }
}
