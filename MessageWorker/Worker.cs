using System.Text;
using System.Text.Json;
using FDX.CommonObjects;

namespace MessageWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions _options;

        public Worker(ILogger<Worker> logger, 
            WorkerOptions options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpClient mqClient = new HttpClient
            {
                BaseAddress = new Uri(_options.MessageUrl)
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var response = await mqClient.GetAsync(_options.GetMessage);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    HttpClient senderClient = new HttpClient
                    {
                        BaseAddress = new Uri(_options.SendMessageUrl)
                    };
                    var json = JsonSerializer.Serialize(new MessageObject { Message = responseBody });
                    await senderClient.PostAsync(_options.SendMessageAPI, new StringContent(json, Encoding.UTF8, "application/json"));
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}