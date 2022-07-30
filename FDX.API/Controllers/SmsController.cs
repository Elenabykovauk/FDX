using FDX.CommonObjects;
using FDX.Services.Interfaces;
using FDX.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FDX.MessageAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;
        private readonly ISmsSendService _smsSendService;
        private readonly ILogger<SmsController> _logger;

        public SmsController(ISmsService smsService, ISmsSendService smsSendService, ILogger<SmsController> logger)
        {
            _smsService = smsService;
            _smsSendService = smsSendService;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSmsList()
        {
            try
            {
                var result = await _smsService.GetSmsList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
        }

        [HttpGet("{smsId}")]
        public async Task<IActionResult> GetSms(Guid smsId)
        {
            try
            {
                var result = await _smsService.GetSms(smsId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SmsDto sms)
        {
            try
            {
                await _smsService.CreateSms(sms);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
        }

        [HttpPost("proceed")]
        public async Task<IActionResult> ProceedSms([FromBody] MessageObject message)
        {
            try
            {
                await _smsSendService.MessageProcceed(message.Message);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest();
        }
    }
}
