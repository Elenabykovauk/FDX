using FDX.Services.Models;

namespace FDX.Services.Interfaces
{
    public interface ISmsService
    {
        Task<IEnumerable<SmsResponse>> GetSmsList();
        Task<SmsResponse> GetSms(Guid smsId);
        Task CreateSms(SmsDto sms);
        Task UpdateSmsStatus(Guid smsId, string status);
        Task<Boolean> ValidateNumbers(IEnumerable<string> numbers);
    }
}
