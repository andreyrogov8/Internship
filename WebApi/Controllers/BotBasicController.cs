using Application;
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
        private readonly TelegramBotClient _telegraBotClient;
        public BotBasicController(TelegramBot telegramBot)
        {
            _telegraBotClient = telegramBot.GetBot().Result;
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] object update)
        {

            var upd = JsonConvert.DeserializeObject<Update>(update.ToString());
            var chat = upd.Message?.Chat;

            if (chat == null) return BadRequest();


            await _telegraBotClient.SendTextMessageAsync(chat.Id, "Hello from CheckInManager Application Layer Bot");

            return Ok();
        }
    }




}
