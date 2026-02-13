using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace AzureOpenAIChatbot.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly OpenAIClient _client;
        private readonly string _deploymentName;

        public OpenAIService(IOptions<AzureOpenAIOptions> options)
        {
            var o = options.Value;
            _client = new OpenAIClient(new Uri(o.Endpoint), new AzureKeyCredential(o.ApiKey));
            _deploymentName = o.DeploymentName;
        }

        public async Task<string> GetChatCompletionAsync(string prompt)
        {
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages = {
                    new ChatMessage(ChatRole.User, prompt)
                },
                MaxTokens = 1000
            };

            try
            {
                Response<ChatCompletions> response = await _client.GetChatCompletionsAsync(_deploymentName, chatCompletionsOptions);
                return response.Value.Choices[0].Message.Content.Trim();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}