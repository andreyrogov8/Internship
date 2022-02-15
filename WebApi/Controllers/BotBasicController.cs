using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WebApi.Controllers
{
    [Route("api/bot")]
    [ApiController]
    public class BotBasicController : ControllerBase
    {
        private readonly ITelegramCommunicationService _telegramCommunicationService;
        public BotBasicController(ITelegramCommunicationService telegramCommunicationService)
        {
            _telegramCommunicationService = telegramCommunicationService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] object update)
        {
            await _telegramCommunicationService.GetMessage(update);

            return Ok();
        }
    }




}
