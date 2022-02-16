using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram
{
    public abstract class CommandBase : KeyboardBase
    {
        public readonly IMediator _mediator;
        public CommandBase(IMediator mediator, TelegramBotClient bot, Message message) : base(bot, message)
        {
            _mediator = mediator;
        }
    }
}

