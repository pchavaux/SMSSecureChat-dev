
    using global::SMSChat.Client.Services;
    using Microsoft.AspNetCore.Mvc;
using SMSChat.Models;


namespace SMSChat.Server.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class SmsController : ControllerBase
        {
            private readonly VoipMsSmsService _smsService;

            public SmsController(VoipMsSmsService smsService)
            {
                _smsService = smsService;
            }

            [HttpPost("send")]
            public async Task<IActionResult> SendSms([FromBody] Client.Services.SmsRequest request)
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Did) ||
                    string.IsNullOrWhiteSpace(request.Dst) || string.IsNullOrWhiteSpace(request.Message))
                {
                    return BadRequest("Invalid SMS request.");
                }

                var success = await _smsService.SendSmsAsync(request.Did, request.Dst, request.Message);
                if (success)
                {
                    return Ok("SMS sent successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to send SMS.");
                }
            }

            [HttpGet("messages")]
            public async Task<ActionResult<IEnumerable<SmsMessage>>> GetMessages([FromQuery] string did)
            {
                if (string.IsNullOrWhiteSpace(did))
                {
                    return BadRequest("DID is required.");
                }

                var messages = await _smsService.GetSmsMessagesAsync(did);
                if (messages != null)
                {
                    return Ok(messages);
                }
                else
                {
                    return StatusCode(500, "Failed to retrieve messages.");
                }
            }
        }
    }
