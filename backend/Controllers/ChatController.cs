using AzureOpenAIChatbot.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureOpenAIChatbot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public ChatController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public class ChatRequest
        {
            public string Message { get; set; } = string.Empty;
        }

        public class ChatResponse
        {
            public string Response { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<ActionResult<ChatResponse>> Post([FromBody] ChatRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                return BadRequest(new ChatResponse { Response = "Message cannot be empty" });
            }

            var result = await _openAIService.GetChatCompletionAsync(request.Message);

            return Ok(new ChatResponse { Response = result });
        }
    }
}