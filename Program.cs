using System.ComponentModel;

internal class Program
{
    private static void Main(string[] args)
    {
        var nums = GetArrayFromFile("path.txt");
    }
    static int[] GetArrayFromFile(string path)
    {
        try
        {
            string content = File.ReadAllText(path);
            var numString = content.Split(" ");
            List<int> nums = new List<int>();
            for (int i = 0; i < numString.Length; i++)
            {
                int num;
                if (int.TryParse(numString[i],out num))
                    nums.Add(num);
            }
            return nums.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine("Не удалось прочитать файл");
            return new int[0];
        }
    }
    static int GetRowCount(string path)
    {
        try
        {
            var rows = File.ReadAllLines(path);
            return rows.Length;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Файл по адресу {path} был не найден");
            return 0;
        }
    }
}