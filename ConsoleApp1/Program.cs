using System.Text;

namespace ConsoleApp1;

internal static class Program
{
    private static async Task Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write("Введите сумму: ");
        var sum = Convert.ToInt32(Console.ReadLine());

        var limitNominal = new Dictionary<int, int>();
        foreach (var row in await File.ReadAllLinesAsync("Money.txt"))
        {
            var rowNumbers = row.Split(" - ").Select(int.Parse).ToArray();
            limitNominal[rowNumbers.First()] = rowNumbers.Last();
        }

        var availableNominal = limitNominal.Keys.ToArray();
        var actualNominal = Calculator.Calc(sum, availableNominal, limitNominal);

        if (actualNominal?.Any() == true)
        {
            foreach (var (key, value) in actualNominal)
            {
                Console.WriteLine($"Nominal: {key}; Count: {value}");
            }
        }
        else
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