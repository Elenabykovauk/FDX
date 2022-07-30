using FDX.Services.Interfaces;

namespace FDX.Services
{
    public class SmsSendService : ISmsSendService
    {
        private readonly ISmsService _smsService;

        public SmsSendService(ISmsService smsService)
        {
            _smsService = smsService;
        }
        public async Task MessageProcceed(string smsId)
        {
            if(!Guid.TryParse(smsId, out Guid id))
                return;

            var message = _smsService.GetSms(id).Result;
            if (_smsService.ValidateNumbers(message.To).Result)
            {
                // TODO send SMS
                _smsService.UpdateSmsStatus(id, "Delivered");
            }
            else
            {
                _smsService.UpdateSmsStatus(id, "failed");
            }
        }
    }
}