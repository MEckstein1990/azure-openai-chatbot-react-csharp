using System.Threading.Tasks;

namespace AzureOpenAIChatbot.Services
{
    public interface IOpenAIService
    {
        Task<string> GetChatCompletionAsync(string prompt);
    }
}