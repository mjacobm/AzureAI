using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Environment;

namespace AzureAIExamples
{
    internal class ClassChatOpenAI_SDK
    {
       public async Task ChatOpenAI_SDKAsync()
        {
            //It suppresses compiler warnings related to Azure OpenAI preview features, acknowledging that the developer is intentionally using APIs that may change in future releases.
            #pragma warning disable AOAI001

            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string endpoint = configuration.GetSection("endpoint").Value;
            string deploymentName = configuration.GetSection("deploymentName").Value;
            string searchEndpoint = configuration.GetSection("searchEndpoint").Value;
            string searchIndex = configuration.GetSection("searchIndex").Value;
            string searchKey = configuration.GetSection("searchKey").Value;

            // Retrieve the OpenAI endpoint from environment variables
            //var endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? "https://mathewjmathew2025-9946-resource.openai.azure.com/";
            if (string.IsNullOrEmpty(endpoint))
            {
                Console.WriteLine("Please set the AZURE_OPENAI_ENDPOINT environment variable.");
                return;
            }

            // Use DefaultAzureCredential for Entra ID authentication
            var credential = new DefaultAzureCredential();

            // Initialize the AzureOpenAIClient
            var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);

            // Initialize the ChatClient with the specified deployment name
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

            // Create a list of chat messages
            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(@"You are an AI assistant that helps people find information."),
                new UserChatMessage("What other certification are you aware of? give me a list?"),
            };


            ChatCompletionOptions options = new();
            options.AddDataSource(new AzureSearchChatDataSource()
            {
                Endpoint = new Uri(searchEndpoint),
                IndexName = searchIndex,
                Authentication = DataSourceAuthentication.FromApiKey(searchKey)
            });

            options.Temperature = 0.7F;
            options.MaxOutputTokenCount = 13107;
            options.TopP = 0.95F;
            options.FrequencyPenalty = 0;
            options.PresencePenalty = 0;


            try
            {
                // Create the chat completion request
               ChatCompletion completion = await chatClient.CompleteChatAsync(messages, options);


             

                // Print the response
                if (completion != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize(completion, new JsonSerializerOptions() { WriteIndented = true }));
                }
                else
                {
                    Console.WriteLine("No response received.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}
