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
                    case "Start":
                        await StartCommand(_telegraBotClient, update.Message);
                        break;
                    case "getworkplaces":
                        await SendWorkplaceList(_mediator, _telegraBotClient, update.Message);
                        break;
                    case "getbookings":                        
                        await SendBookingList(_mediator, _telegraBotClient, update.Message);
                        break;
                    default:
                        await DefaultHandler(_telegraBotClient, update.Message);
                        break;
                }
                return;
            }            
        }
        public static async Task<Message> StartCommand(TelegramBotClient bot, Message message)
        {
            var commandNames = new List<string>();
            commandNames.Add("getworkplaces");
            commandNames.Add("getbookings");
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            var counter = 0;
            foreach (var name in commandNames)
            {
                counter++;
                cols.Add(new KeyboardButton($"{name}"));
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
            rmk.OneTimeKeyboard = true;
            return await bot.SendTextMessageAsync(
                message.Chat.Id,
                "Press Button",
                replyMarkup: rmk
                );
        }
        public static async Task<Message> SendWorkplaceList(IMediator mediator, TelegramBotClient bot, Message message)
        {
            var workplaceResponse = await mediator.Send(new GetWorkplaceListQueryRequest());

            var workplaces = workplaceResponse.Results;
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            var counter = 0;
            foreach (var workplace in workplaces)
            {
                counter++;
                cols.Add(new KeyboardButton($"Id: {workplace.Id}, WorkplaceNumber: {workplace.WorkplaceNumber}"));
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
            rmk.OneTimeKeyboard = true;
            return await bot.SendTextMessageAsync(
                message.Chat.Id,
                "Workplace List",
                replyMarkup: rmk
                );
        }
        public static async Task<Message> SendBookingList(IMediator mediator, TelegramBotClient bot, Message message)
        {
            var bookingResponse = await mediator.Send(new GetBookingListQueryRequest());

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
            rmk.OneTimeKeyboard = true;
            return await bot.SendTextMessageAsync(
                message.Chat.Id,
                "This is Booking List: ",
                replyMarkup: rmk
                );
        }

        public static async Task<Message> DefaultHandler(TelegramBotClient bot, Message message)
        {
            var rows = new List<KeyboardButton>();
                rows.Add($"Start");

            var keyboard = new ReplyKeyboardMarkup(rows);
            keyboard.OneTimeKeyboard = true;
            keyboard.ResizeKeyboard = true;
            return await bot.SendTextMessageAsync(message.Chat.Id, "To start communication please press start button",
                                     replyMarkup: keyboard);
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
