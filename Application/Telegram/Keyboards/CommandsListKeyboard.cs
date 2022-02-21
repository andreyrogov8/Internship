using Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class CommandsListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<string> commandNames, int columns)
        {
            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton(name) { CallbackData = name});
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, columns);
            return inlineKeyboard;
        }
        public static InlineKeyboardMarkup BuildKeyboard(List<string> commandNames, int columns, UserState currentState)
        {
            commandNames.Add("◀️Go Back");
            var inlineKeyboard = CommandsListKeyboard.BuildKeyboard(commandNames, columns);
            inlineKeyboard.InlineKeyboard.ToList().Last().ToList().Last().CallbackData = $"goBack|{currentState.ToString()}";
            return inlineKeyboard;
        }
    }
}
