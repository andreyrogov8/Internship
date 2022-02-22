using Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class CommandsListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<string> commandNames, int columns, bool isStart=false)
        {
            if (!isStart)
            {
                commandNames.Add("◀️Go Back");
            }

            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton(name) { CallbackData = name});
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, columns);
            inlineKeyboard.InlineKeyboard.ToList().Last().ToList().Last().CallbackData = $"goBack";

            return inlineKeyboard;
        }
       
    }
}
