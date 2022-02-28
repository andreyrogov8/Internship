using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Application.Interfaces
{
    public interface ITelegramCommunicationService
    {
        public Task ExecuteAsync(Update update);
    }
}
