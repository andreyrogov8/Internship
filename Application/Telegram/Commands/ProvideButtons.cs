﻿using System;
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
        //public CallbackQuery _callbackQuery;
        List<string> commandNames = new List<string>();
        public ProvideButtons(TelegramBotClient bot)
        {
            _bot = bot;
            //_callbackQuery = callbackQuery;
        }

        public async Task Send(CallbackQuery callbackQuery,List<string> commandNames, int numberOfColumns)
        {
            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton($"{name}") { CallbackData = name });
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, numberOfColumns);
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
        }
        public async Task Send(Message message, List<string> commandNames, int numberOfColumns)
        {
            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton($"{name}") { CallbackData = name });
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, numberOfColumns);
            await _bot.SendTextMessageAsync(message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
        }
    }
}
