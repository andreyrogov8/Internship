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
    }
}
