using System.Text.Json.Serialization;

namespace FDX.Services.Models
{
    public class SmsDto
    {
        public Guid Id { get; set; } 
        public string From { get; set; }
        public string[] To { get; set; }
        public string Content { get; set; }
    }
}