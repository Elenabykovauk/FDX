using System.Collections.Concurrent;
using FDX.CommonObjects;
using Microsoft.AspNetCore.Mvc;

namespace MQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageQueueController : ControllerBase
    {
        private static ConcurrentQueue<object> concurrentQueue = new ConcurrentQueue<object>();

        private readonly ILogger<MessageQueueController> _logger;

        public MessageQueueController(ILogger<MessageQueueController> logger)
        {
            _logger = logger;
        }

        [HttpPost("AddMessage")]
        public void Add([FromBody] MessageObject message)
        {
            concurrentQueue.Enqueue(message.Message);
        }

        [HttpGet("GetMessage")]
        public object Get()
        {
            concurrentQueue.TryDequeue(out object message);
            return message;
        }
    }
}