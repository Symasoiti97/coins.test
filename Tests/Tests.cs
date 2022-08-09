using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using FluentAssertions;
using NUnit.Framework;

namespace Tests;

public class Tests
{
    [TestCaseSource(typeof(TestSource), nameof(TestSource.Calc_Test_Source))]
    public void Calc_Test(int sum, Dictionary<int, int> limitNominal, Dictionary<int, int> exceptedNominal)
    {
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

        actualNominal.Should().Equal(exceptedNominal);
    }

    private static class TestSource
    {
        public static IEnumerable<object[]> Calc_Test_Source()
        {
            yield return new object[]
            {
                1500,
                new Dictionary<int, int> {[50] = 5, [20] = 20, [100] = 10},
                new Dictionary<int, int> {[20] = 20, [50] = 4, [100] = 9}
            };

            yield return new object[]
            {
                1500,
                new Dictionary<int, int> {[100] = 10, [50] = 5, [20] = 20},
                new Dictionary<int, int> {[20] = 15, [50] = 4, [100] = 10}
            };
        }
    }
}