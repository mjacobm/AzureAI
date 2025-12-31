
using AzureAIExamples;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("----- ClassChatOpenAI_SDK EXAMPLE -----");
        var chatOpenAISdk = new ClassChatOpenAI_SDK();
        await chatOpenAISdk.ChatOpenAI_SDKAsync();

        Console.WriteLine("----- ClassChatAzureOpenAI_SDK EXAMPLE -----");
        var chatAzureSdk = new ClassChatAzureOpenAI_SDK();
        chatAzureSdk.ChatAzureOpenAI_SDK();

        Console.WriteLine("Main completed");
    }
}

//// Install the .NET library via NuGet: dotnet add package Azure.AI.OpenAI --prerelease
//using Azure;
//using Azure.AI.OpenAI;
//using Azure.AI.OpenAI.Chat;
//using Azure.Identity;
//using OpenAI.Chat;
//using System.Text.Json;
////using OpenAI.Chat;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Resources;
//using System.Security.Principal;
//using System.Threading.Tasks;
//using static System.Environment;
//using static System.Net.Mime.MediaTypeNames;
//using static System.Reflection.Metadata.BlobBuilder;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using Microsoft.Extensions.Configuration;

//var builder = new ConfigurationBuilder();
//IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json")
//    .Build();

//string endpoint = configuration.GetSection("endpoint").Value;
//string deploymentName = configuration.GetSection("deploymentName").Value;
//string searchEndpoint = configuration.GetSection("searchEndpoint").Value; 
//string searchIndex = configuration.GetSection("searchIndex").Value;

//string systemPrompt = """
//You are an AI assistant that helps users prepare for the AI-102 "Azure AI Engineer Associate" certification (formerly known as AI201).
//Your task is to:
//- Identify and explain the key topics and skills users should focus on ("what to stress") for the AI-102 exam
//- Provide links to official study resources (Microsoft Learn, documentation, practice exams)
//- Offer practical, real-world examples for each key topic
//Responses must be structured, concise, and actionable.
//""";
//#pragma warning disable AOAI001
//AzureOpenAIClient azureClient = new(
//    new Uri(endpoint),
//    new DefaultAzureCredential()
//    );

//ChatClient chatClient = azureClient.GetChatClient(deploymentName);

//ChatCompletionOptions options = new();
//options.AddDataSource(new AzureSearchChatDataSource()
//{
//    Endpoint = new Uri(searchEndpoint),
//    IndexName = searchIndex,
//    Authentication = DataSourceAuthentication.FromApiKey("lH0x6AJkhxOlOAnU2YHH0D28gh6TsA0UQ1475dZ83VAzSeDCVOK0")
//});

//options.Temperature = 0.7F;
//options.MaxOutputTokenCount = 13107;
//options.TopP = 0.95F;
//options.FrequencyPenalty = 0;
//options.PresencePenalty = 0;


//ChatCompletion completion = chatClient.CompleteChat(
//    [
//        new SystemChatMessage(systemPrompt),
//        new UserChatMessage("after how many months should i renew my certificate?"),
//    ]
//    ,options);




//// Print the response
//if (completion != null)
//{
//    Console.WriteLine(JsonSerializer.Serialize(completion, new JsonSerializerOptions() { WriteIndented = true }));
//}
//else
//{
//    Console.WriteLine("No response received.");
//}

//IGNORE
//ChatMessageContext onYourDataContext = completion.GetMessageContext();

//if (onYourDataContext?.Intent is not null)
//{
//    Console.WriteLine($"Intent: {onYourDataContext.Intent}");
//}
//foreach (ChatCitation citation in onYourDataContext?.Citations ?? new List<ChatCitation>())
//    {
//    Console.WriteLine($"Citation: {citation.Content}");
//}



//EXAMPLE 1

//// Install the .NET library via NuGet: dotnet add package Azure.AI.OpenAI --prerelease
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Azure;
//using Azure.AI.OpenAI;
//using Azure.Identity;
//using OpenAI.Chat;

//using static System.Environment;
//using System.Text.Json;

//async Task RunAsync()
//{
//    // Retrieve the OpenAI endpoint from environment variables
//    var endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? "https://mathewjmathew2025-9946-resource.openai.azure.com/";
//    if (string.IsNullOrEmpty(endpoint))
//    {
//        Console.WriteLine("Please set the AZURE_OPENAI_ENDPOINT environment variable.");
//        return;
//    }

//    // Use DefaultAzureCredential for Entra ID authentication
//    var credential = new DefaultAzureCredential();

//    // Initialize the AzureOpenAIClient
//    var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);

//    // Initialize the ChatClient with the specified deployment name
//    ChatClient chatClient = azureClient.GetChatClient("gpt-4.1");

//    // Create a list of chat messages
//    var messages = new List<ChatMessage>
//    {
//        new SystemChatMessage(@"You are an AI assistant that helps people find information."),
//    };

//    // Create chat completion options
//    var options = new ChatCompletionOptions
//    {
//        Temperature = (float)0.7,
//        MaxOutputTokenCount = 13107,

//        TopP = (float)0.95,
//        FrequencyPenalty = (float)0,
//        PresencePenalty = (float)0
//    };

//    try
//    {
//        // Create the chat completion request
//        ChatCompletion completion = await chatClient.CompleteChatAsync(messages, options);

//        // Print the response
//        if (completion != null)
//        {
//            Console.WriteLine(JsonSerializer.Serialize(completion, new JsonSerializerOptions() { WriteIndented = true }));
//        }
//        else
//        {
//            Console.WriteLine("No response received.");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"An error occurred: {ex.Message}");
//    }
//}

//await RunAsync();