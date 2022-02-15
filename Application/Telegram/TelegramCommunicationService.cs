using Application.Features.BookingFeature.Queries;
using Application.Features.CountryCQ;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
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
            await _telegraBotClient.SendChatActionAsync(update.Message.Chat.Id, ChatAction.Typing);
            if (update.Message != null)
            {
                switch (update.Message.Text)
                {
                    case "/start":
                        await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, "To get all workplaces use command /getworkplaces");
                        break;
                    case "/getworkplaces":
                        var result = await _mediator.Send(new GetWorkplaceListQueryRequest());
                        foreach (var item in result.Results)
                        {
                            await _telegraBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"WorkplaceId:{item.Id},WorkplaceNumber:{item.WorkplaceNumber},");
                        }
                        break;
                    case "/bookings":
                        
                        var bookingResponse = await _mediator.Send(new GetBookingListQueryRequest());
                        await SendBookingList(_telegraBotClient, update.Message, bookingResponse);
                        break;
                    default:
                        await DefaultHandler(_telegraBotClient, update.Message);
                        break;
                }
                return;
            }            
        }
        public static async Task<Message> SendBookingList(TelegramBotClient bot, Message message, GetBookingListQueryResponse bookingResponse)
        {
            var bookings = bookingResponse.Results;
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            var counter = 0;
            foreach(var booking in bookings)
            {
                counter++;
                cols.Add(new KeyboardButton($"Owner: {booking.UserName}, Work Place ID: {booking.WorkplaceId}"));
                if (counter % 2 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }
            if (cols.Count > 0)
            {
                rows.Add(cols.ToArray());

            }
            var rmk = new ReplyKeyboardMarkup(rows);
            rmk.ResizeKeyboard = true;
            return await bot.SendTextMessageAsync(
                message.Chat.Id,
                "This is Booking List: ",
                replyMarkup: rmk
                );
        }

        public static async Task<Message> DefaultHandler(TelegramBotClient bot, Message message)
        {
            return await bot.SendTextMessageAsync(
                message.Chat.Id,
                "Hello, Right now we are working on features.\nAvailable commands:\n/start\n/getworkplaces\n/bookings"
                );
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
