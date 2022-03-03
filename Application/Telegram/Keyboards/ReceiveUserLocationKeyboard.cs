using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class ReceiveUserLocationKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard()
        {
            var buttons = new List<KeyboardButton>();
            buttons.Add(KeyboardButton.WithRequestLocation("Send my location"));
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 1);
            return replyKeyboard;
        }
    }
}
