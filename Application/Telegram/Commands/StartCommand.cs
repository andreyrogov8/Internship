using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class StartCommand
    {
        public TelegramBotClient _bot;
        public readonly IMediator _mediator;
        public StartCommand(IMediator mediator, TelegramBotClient bot)
        {
            _bot = bot;
            _mediator = mediator;
        }
        public async Task HandleAsync(Update update)
        {
            switch(update.Message.Text)
            {
                case "/help":
                    await _bot.SendTextMessageAsync(update.Message.Chat.Id, $"Hi {update.Message.From.FirstName}, we are working on features. Available commands\n/help - for some instructions\n/start - for start booking workplace");
                    // we should not update the user's state if he types /help
                    UserStateStorage.UpdateUserState(update.Message.From.Id, UserState.ProcessNotStarted);

                    return;
                case "/start":
                default:
                    await new ProvideButtons(_bot).SendAsync(update.Message, new List<string>() { "Start" }, 1);
                    return;
            }
        }
    }
}
