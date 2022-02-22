﻿using Application.Telegram.Keyboards;
using Domain.Enums;
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

        public async Task Send(CallbackQuery callbackQuery, List<string> commandNames, int numberOfColumns)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns);
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
        }
        public async Task Send(Message message, List<string> commandNames, int numberOfColumns, bool isStart=false)
        {
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, numberOfColumns, isStart);
            await _bot.SendTextMessageAsync(message.Chat.Id, "Press Button", replyMarkup: inlineKeyboard);
        }
    }
}
