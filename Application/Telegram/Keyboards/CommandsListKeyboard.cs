using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class CommandsListKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard(List<string> commandNames)
        {
            var buttons = new List<KeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new KeyboardButton(name));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 2);
            return replyKeyboard;
        }
    }
}
