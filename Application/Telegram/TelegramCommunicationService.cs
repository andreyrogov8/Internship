using Application.Features.CountryCQ;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Application.TelegramBot
{
    public class TelegramCommunicationService : ITelegramCommunicationService
    {
        private readonly TelegramBotClient _telegraBotClient;
        private readonly IMediator _mediator;
        public TelegramCommunicationService(IConfiguration configuration, IMediator mediator)
        {
            _telegraBotClient = new TelegramBotClient(configuration["Token"]);
            _mediator   = mediator;
        }

        public async Task Execute(Update update)
        {
            if (update.Message != null && update.Message.Text.Contains("/start"))
            {
                await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, "To get all workplaces use command /getworkplaces");
            }
            if (update.Message != null && update.Message.Text.Contains("/getworkplaces"))
            {
                var result = await _mediator.Send(new GetWorkplaceListQueryRequest());
                foreach (var item in result.Results)
                {
                    await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"WorkplaceId:{item.Id},WorkplaceNumbre:{item.WorkplaceNumber},");
                }                
            }
        }

        public async Task GetMessage(object update)
        {
            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());
            var chat = upd.Message?.Chat;

            if (chat == null) throw new ValidationException($"No message");
            await _telegraBotClient.SendTextMessageAsync(chat.Id, "Hello from CheckInManager Application Layer Bot");
        }

    }
}
