using Application.Telegram.Helpers;
using Application.Telegram.Keyboards;
using Domain.Enums;
using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram.Commands
{
    public class ReceiveUserLocationCommand
    {
        private readonly IMediator _mediator;
        private readonly TelegramBotClient _bot;
        private readonly IHttpClientFactory _clientFactory;

        public ReceiveUserLocationCommand(IMediator mediator, TelegramBotClient bot, IHttpClientFactory clientFactory)
        {
            _mediator = mediator;
            _bot = bot;
            _clientFactory = clientFactory;
        }

        public async Task SendAsync(CallbackQuery callbackQuery)
        {
            var keyboard = ReceiveUserLocationKeyboard.BuildKeyboard();
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "So, you want me to suggest offices using your location. Please, press this button and send me your location.", replyMarkup: keyboard);
        }
        public async Task ReceiveLocationAndSendOffices(Message message)
        {
            var latitude = message.Location.Latitude;
            var longitude = message.Location.Longitude;
            var countryName = await new IdentifyUserLocation(_clientFactory).FindCountryAsync(longitude, latitude);
            await new SendOfficeListCommand(_mediator, _bot).Send(message, query: countryName, sendedQuery:true);
            UserStateStorage.UserStateUpdate(message.From.Id, UserState.NewBookingIsSelectedStartingBooking);
        }

        
        
    }
}
