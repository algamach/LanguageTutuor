using LanguageTutuor;

internal class Program
{
    private static void Main(string[] args)
    {
       
        var tutor = new Tutor();

        tutor.AddWord("dog", "собака");
        tutor.AddWord("cat", "кошка");
        tutor.AddWord("hello", "привет");
        tutor.AddWord("bye", "пока");
        tutor.AddWord("bike", "велосипед");

        while (true)
        {
            string word = tutor.GetRandomEngWord();
            Console.WriteLine(word);
            Console.Write("Введите перевод: ");
            string answer = Console.ReadLine();
            if (tutor.CheckWord(word,answer))
            {
                Console.WriteLine($"Вы правы {word} переводится как как {tutor.Translate(word)}");
            }
            else
            {
                Console.WriteLine($"К сожалению вы ошиблись {word} переводится как как {tutor.Translate(word)}");
            }
            Console.WriteLine();
        }
    }
}