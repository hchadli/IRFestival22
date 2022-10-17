// See https://aka.ms/new-console-template for more information

using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using IRFestival.MailerConsole;

Console.WriteLine("Hello, World!");

Console.WriteLine("Hello, I'm Mailer Console Application");

var connectionString = "Endpoint=sb://irfestivalservicebusch.servicebus.windows.net/;SharedAccessKeyName=listener;SharedAccessKey=qvKaksvdg+T0VlmovzH7U3KoQbJx897U0V5g/hMhjPM=";
var queueName = "mails";

await using (var client = new ServiceBusClient(connectionString))
{
    var processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());


    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;

    await processor.StartProcessingAsync();

    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();


    Console.WriteLine("\nStopping the receiver ...");
    await processor.StopProcessingAsync();

    Console.WriteLine("Stopped receiving messages");
}



static async Task MessageHandler(ProcessMessageEventArgs args)
{
    var body = args.Message.Body.ToString();
    var message = JsonSerializer.Deserialize<Mailer>(body);

    SendMail("madi.chadli@gmail.com", message.Email);
    await args.CompleteMessageAsync(args.Message);
}

static async Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    await Task.CompletedTask;
}


static void SendMail(string senderEmail, string receiverEmail)   
{
    var client = new SmtpClient("smtp.gmail.com", 587)
    {
        Credentials = new NetworkCredential(senderEmail, "bbrhodlwwvmwvvqy"),
        EnableSsl = true
    };
    client.Send(senderEmail, receiverEmail,"Test" , "Test");
    Console.WriteLine("Email Sent to " + receiverEmail);
    Console.ReadLine();
}
