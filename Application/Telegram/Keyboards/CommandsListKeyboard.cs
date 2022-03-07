using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class CommandsListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<string> commandNames, int columns, string commandCall=null)
        {
            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                if (name == "Start New" && commandCall != null)
                {
                    buttons.Add(new InlineKeyboardButton(name) { CallbackData = commandCall });
                }
                else if (name.Contains("BACK"))
                {
                    buttons.Add(new InlineKeyboardButton("BACK") { CallbackData = $"BACK{commandCall}" });
                }
                else
                {
                    buttons.Add(new InlineKeyboardButton(name) { CallbackData = name });
                }
                
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, columns);
            return inlineKeyboard;
        }
        public static InlineKeyboardMarkup BuildKeyboard(List<Tuple<string,string>> commandNames, int columns)
        {
            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton(name.Item1) { CallbackData = name.Item2 });
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, columns);
            return inlineKeyboard;
        }
    }
}
