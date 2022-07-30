using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using FDX.CommonObjects;
using FDX.DataAccess.Models;
using FDX.DataAccess.Repositories.Interfaces;
using FDX.Services.Interfaces;
using FDX.Services.Models;
using Microsoft.Extensions.Options;

namespace FDX.Services
{
    public class SmsService : ISmsService
    {
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<MessageQueueOptions> _messageQueueOptions;
        private readonly HttpClient _client;

        public SmsService(ISmsRepository smsRepository,
            IMapper mapper,
            IOptions<MessageQueueOptions> messageQueueOptions)
        {
            _smsRepository = smsRepository;
            _mapper = mapper;
            _client = new HttpClient
            {
                BaseAddress = new Uri(messageQueueOptions.Value.MessageUrl)
            };
            _messageQueueOptions = messageQueueOptions;
        }

        public async Task<IEnumerable<SmsResponse>> GetSmsList()
        {
            var result = await _smsRepository.GetAsync();
            result.ToList().ForEach(x => x.Content = "");

            return _mapper.Map<List<SmsResponse>>(result);
        }
        public async Task<SmsResponse> GetSms(Guid smsId)
        {
            var result = await _smsRepository.GetByIdAsync(smsId);
            return _mapper.Map<SmsResponse>(result);
        }
        public async Task CreateSms(SmsDto sms)
        {
            var entity = _mapper.Map<Sms>(sms);
            entity.Status = "New";
            await _smsRepository.InsertAsync(entity);
            await _smsRepository.SaveAsync();

            await SendMessageToMQ(entity.Id);
        }
        public async Task UpdateSmsStatus(Guid smsId, string status)
        {
            var entity = await _smsRepository.GetByIdAsync(smsId);
            entity.Status = status;
            await _smsRepository.SaveAsync();
        }
        public async Task<Boolean> ValidateNumbers(IEnumerable<string> numbers)
        {
            var regex = new Regex("^[0-9]+$", RegexOptions.IgnoreCase);

            foreach (var number in numbers)
            {
                if (!regex.Match(number).Success)
                    return false;
            }
            return true;

        }
        private async Task SendMessageToMQ(Guid smsId)
        {
            var json = JsonSerializer.Serialize(new MessageObject { Message = smsId.ToString() });
            await _client.PostAsync(_messageQueueOptions.Value.AddMessage, new StringContent(json, Encoding.UTF8, "application/json"));
        }
    }
}