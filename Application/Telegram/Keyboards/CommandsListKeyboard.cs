using Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class CommandsListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(List<string> commandNames, int columns, bool isStart=false)
        {
            

            var buttons = new List<InlineKeyboardButton>();
            foreach (var name in commandNames)
            {
                buttons.Add(new InlineKeyboardButton(name) { CallbackData = name});
            }
            if (!isStart)
            {
                buttons.Add((new InlineKeyboardButton("◀️Go Back") { CallbackData = "goBack" }));
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, columns);

            return inlineKeyboard;
        }
       
    }
}
