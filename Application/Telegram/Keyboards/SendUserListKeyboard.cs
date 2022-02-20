using Application.Features.UserFeature.Queries;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.Telegram.Keyboards
{
    public static class SendUserListKeyboard
    {
        public static InlineKeyboardMarkup BuildKeyboard(IEnumerable<UserDTO> users)
        {
            var buttons = new List<InlineKeyboardButton>();

            foreach (var user in users)
            {
                buttons.Add(new InlineKeyboardButton($"Name: {user.FirstName} {user.LastName}") { CallbackData = user.Id.ToString()});
            }
            var inlineKeyboard = KeyboardHelper.BuildInLineKeyboard(buttons, 1);
            return inlineKeyboard;
        }
    }
}
