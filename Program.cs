using System.Text;

namespace ConsoleApp1;

internal static class Program
{
    private static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Введите сумму: ");
        var sum = Convert.ToInt32(Console.ReadLine());

        var numbers = new List<int>();
        foreach (var row in await File.ReadAllLinesAsync("Money.txt"))
        {
            var rowNumbers = row.Split(" - ").Select(int.Parse).ToArray();
            for (var i = 0; i < rowNumbers[1]; i++)
            {
                numbers.Add(rowNumbers[0]);
            }
        }

        var isExits = false;
        foreach (var combination in CreateCombinations(0, new List<int>(), numbers))
        {
            if (sum == combination.Sum())
            {
                isExits = true;
                Console.WriteLine(string.Join(", ", combination));
            }
        }

        if (isExits == false)
        {
            Console.WriteLine("Не найдено");
        }
    }

    private static List<List<int>> CreateCombinations(int startIndex, IReadOnlyCollection<int> pair, IReadOnlyList<int> initialArray)
    {
        var combinations = new List<List<int>>();
        for (var i = startIndex; i < initialArray.Count; i++)
        {
            var value = new List<int>(pair) {initialArray[i]};
            combinations.Add(value);
            combinations.AddRange(CreateCombinations(i + 1, value, initialArray));
        }

        return combinations;
    }
}