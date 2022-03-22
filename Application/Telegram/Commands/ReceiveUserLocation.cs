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
            var currentMessage = await _bot.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                "Suggest offices by location. Please send me your location.",
                replyMarkup: keyboard);
            UserStateStorage.AddMessage(callbackQuery.From.Id, currentMessage.MessageId);
        }
        public async Task ReceiveLocationAndSendOffices(Message message)
        {
            if (message.Location is null)
            {
                await _bot.DeleteMessageAsync(message.From.Id, message.MessageId);
                await new ProvideButtons(_bot).SendAsync(
                        message
                        , new List<string> { "New Booking" }
                        , 1);
                UserStateStorage.UpdateUserState(message.From.Id, UserState.SelectingAction);
            }
            else 
            {
               var latitude = message.Location.Latitude;
               var longitude = message.Location.Longitude;
               var countryName = await new IdentifyUserLocation(_clientFactory).FindCountryAsync(longitude, latitude);
               await new SendOfficeListCommand(_mediator, _bot).Send(message, query: countryName, sendedQuery:true);
               UserStateStorage.UpdateUserState(message.From.Id, UserState.NewBookingIsSelectedStartingBooking);
            }
        }

        
        
    }
}
