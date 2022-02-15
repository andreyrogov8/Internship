using Application.Interfaces;
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

namespace Application.TelegramBot
{
    public class TelegramCommunicationService : ITelegramCommunicationService
    {
        private readonly IConfiguration _configuration;
        private readonly TelegramBotClient _telegraBotClient;
        public TelegramCommunicationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _telegraBotClient = new TelegramBotClient(_configuration["Token"]);
        }

        public Task GetMessage(Update update)
        {
            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());
            var chat = upd.Message?.Chat;

            if (chat == null) throw new ValidationException($"No message");
            _telegraBotClient.SendTextMessageAsync(chat.Id, "Hello from CheckInManager Application Layer Bot");

            return Task.CompletedTask;
        }

    }
}
