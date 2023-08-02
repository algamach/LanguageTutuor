using Telegram.Bot;

namespace LanguageTutuor;
internal class Program
{
    static TelegramBotClient Bot;
    static Tutor Tutor = new Tutor();
    static Dictionary<int, string> LastWord = new Dictionary<int, string>();
    const string COMMAND_LIST =
@"Список команд:
/add <eng> <rus> - добавление английского слова и его перевод в словарь 
/get - получаем случайное английское слово из словаря
/check <eng> <rus> - проверяем правильность перевода  английского слова 
";
    private static void Main(string[] args)
    {
        string token = File.ReadAllText("token.txt");
        Bot = new TelegramBotClient(token);

        Bot.OnMessage += Bot_OnMessage;
        Bot.StartReceiving();
        Console.ReadLine();
        Bot.StopReceiving();
    }

    private static async void Bot_OnMessage(object? sender, Telegram.Bot.Args.MessageEventArgs e)
    {
        if (e == null || e.Message == null || e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            return;

        Console.WriteLine(e.Message.Text);
        Console.WriteLine(e.Message.From.Username);

        var msgArr = e.Message.Text.Split(' ');
        var userId = e.Message.From.Id;
        String text = " ";
        switch (msgArr[0])
        {
            case "/start":
                text = COMMAND_LIST;
                break;
            case "/add":
                text = AddWords(msgArr);
                break;
            case "/get":
                text = GetRandomEngWord(userId);
                break;
            case "/check":
                text = CheckWord(msgArr);
                var newWord = GetRandomEngWord(userId);
                text = $"{text}\r\nСледующее слово: {newWord}";
                break;
            default:
                if (LastWord.ContainsKey(userId))
                {
                    text = CheckWord(LastWord[userId], msgArr[0]);
                    newWord = GetRandomEngWord(userId);
                    text = $"{text}\r\nСледующее слово: {newWord}";
                }
                else
                {
                    text = COMMAND_LIST;
                }
                break;

        }
        await Bot.SendTextMessageAsync(e.Message.From.Id, text);
    }

    private static string GetRandomEngWord(int userId)
    {
        var text = Tutor.GetRandomEngWord();
        if (LastWord.ContainsKey(userId))
            LastWord[userId] = text;
        else
            LastWord.Add(userId, text);
        return text;
    }

    private static string CheckWord(string[] msgArr)
    {
        if (msgArr.Length != 3)
            return "Неправильное количество аргументов. Их должно быть 2";
        else
            return CheckWord(msgArr[1], msgArr[2]);
    }

    private static string CheckWord(string eng, string rus)
    {
        if (Tutor.CheckWord(eng, rus))
            return "Правильно!";
        else
        {
            string correctAnswer = Tutor.Translate(eng);
            return $"Неверно. Правильный ответ: \"{correctAnswer}\"";
        }
    }

    static string AddWords(String[] msgArr)
    {
        if (msgArr.Length != 3)
        {
            return "Неправильное количество аргументов. Их должно быть 2";
        }
        else
        {
            Tutor.AddWord(msgArr[1], msgArr[2]);
            return "Новое слово добавлено";
        }
    }
}

