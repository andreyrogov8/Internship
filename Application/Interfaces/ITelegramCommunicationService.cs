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
        public Task GetMessage(object update);
        public Task Execute(Update update);
    }
}
