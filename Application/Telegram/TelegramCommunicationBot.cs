using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application
{
    public class TelegramCommunicationBot : ITelegramCommunicationService
    {
        private readonly IConfiguration _configuration;
        private TelegramBotClient _botClient;
        public TelegramCommunicationBot(IConfiguration configuration)
        {
            _configuration = configuration;
            _botClient = new TelegramBotClient(_configuration["Token"]);
        }
        //public async Task<TelegramBotClient> GetBot()
        //{
        //    _botClient = new TelegramBotClient(_configuration["Token"]);

        //    //var hook = $"{_configuration["Url"]}api/bot/update";
        //    //await _botClient.SetWebhookAsync(hook);

        //    return _botClient;
        //}

        public void GetMessage(Update update)
        {
            throw new NotImplementedException();
        }
    }
}
