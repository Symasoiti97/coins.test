namespace ConsoleApp1;

public static class Calculator
{
    public static IDictionary<int, int>? Calc(int sum, int[] availableNominal, IDictionary<int, int> limitNominal)
    {
        if (sum == 0)
            return new Dictionary<int, int>();

        if (availableNominal.Length == 0)
        {
            return null;
        }

        var currentNominal = availableNominal.First();
        var availableNominalCount = Math.Min(limitNominal[currentNominal], sum / currentNominal);

        for (var i = availableNominalCount; i >= 0; i--)
        {
            var result = Calc(sum - i * currentNominal, availableNominal.Skip(1).ToArray(), limitNominal);

            if (result != null)
            {
                if (i > 0)
                {
                    result[currentNominal] = i;
                }

                return result;
            }
        }

        return null;
    }
}