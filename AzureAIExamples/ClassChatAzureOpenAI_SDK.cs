// Install the .NET library via NuGet: dotnet add package Azure.AI.OpenAI --prerelease
using Azure;
using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using Azure.Identity;
//using System.Text.Json;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Security.Principal;
using System.Threading.Tasks;
using static System.Environment;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace AzureAIExamples
{
    internal class ClassChatAzureOpenAI_SDK
    {
        public void ChatAzureOpenAI_SDK()
        {
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string endpoint = configuration.GetSection("endpoint").Value;
            string deploymentName = configuration.GetSection("deploymentName").Value;
            string searchEndpoint = configuration.GetSection("searchEndpoint").Value;
            string searchIndex = configuration.GetSection("searchIndex").Value;
            string searchKey = configuration.GetSection("searchKey").Value;

            string systemPrompt = """
            You are an AI assistant that helps users prepare for the AI-102 "Azure AI Engineer Associate" certification (formerly known as AI201).
            Your task is to:
            - Identify and explain the key topics and skills users should focus on ("what to stress") for the AI-102 exam
            - Provide links to official study resources (Microsoft Learn, documentation, practice exams)
            - Offer practical, real-world examples for each key topic
            Responses must be structured, concise, and actionable.
            """;
            #pragma warning disable AOAI001
            AzureOpenAIClient azureClient = new(
                new Uri(endpoint),
                new DefaultAzureCredential()
                );

            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

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


            ChatCompletion completion = chatClient.CompleteChat(
                [
                    new SystemChatMessage(systemPrompt),
                    new UserChatMessage("after how many months should i renew my certificate?"),
                ]
                , options);


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
    }
}
