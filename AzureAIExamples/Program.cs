
using AzureAIExamples;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("----- ClassChatOpenAI_SDK EXAMPLE -----");
        var chatOpenAISdk = new ClassChatOpenAI_SDK();
        await chatOpenAISdk.ChatOpenAI_SDKAsync();

        //Console.WriteLine("----- ClassChatAzureOpenAI_SDK EXAMPLE -----");
        //var chatAzureSdk = new ClassChatAzureOpenAI_SDK();
        //chatAzureSdk.ChatAzureOpenAI_SDK();

        Console.WriteLine("Main completed");
    }
}
