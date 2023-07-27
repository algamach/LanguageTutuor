using System.ComponentModel;
using Telegram.Bot;

internal class Program
{
    static TelegramBotClient Bot;
    private static void Main(string[] args)
    {
        string token = File.ReadAllText("token.txt");
        Bot = new TelegramBotClient(token);

        Bot.OnMessage += Bot_OnMessage;
        Bot.StartReceiving();
        Console.ReadLine();
        Bot.StopReceiving();
    }

    private static void Bot_OnMessage(object? sender, Telegram.Bot.Args.MessageEventArgs e)
    {
        Console.WriteLine(e.Message.Text);
        Console.WriteLine(e.Message.From.Username);
    }
}