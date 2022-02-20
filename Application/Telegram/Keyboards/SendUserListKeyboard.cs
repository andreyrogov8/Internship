using Application.Features.UserFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendUserListKeyboard
    {
        public static ReplyKeyboardMarkup BuildKeyboard(IEnumerable<UserDTO> users)
        {
            var buttons = new List<KeyboardButton>();

            foreach (var user in users)
            {
                buttons.Add(new KeyboardButton($"Id: {user.Id}, Name: {user.FirstName} {user.LastName}"));
            }
            var replyKeyboard = KeyboardHelper.BuildKeyboard(buttons, 1);
            return replyKeyboard;
        }
    }
}
