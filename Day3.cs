using System.Data;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2023;

public partial class Day3 : IDay
{
    private readonly string _inputFile = "Input/Day_3_Input.txt";
    private readonly int _partOneAnswer;
    private readonly int _partTwoAnswer;

    public Day3()
    {
        var input = File.ReadAllLines(_inputFile);

        _partOneAnswer = Part1(input);
        _partTwoAnswer = Part2(input);
    }

    public void Run()
    {
        Console.WriteLine($"Part 1 - The sum of all the part numbers in the engine schematic is: {_partOneAnswer}");
        Console.WriteLine($"Part 2 - The sum of all the gear ratios in the engine schematic is: {_partTwoAnswer}");
    }

    private int Part1(string[] engineSchematic)
    {
        var symbols = Parse(engineSchematic, SymbolRegex());
        var partNumbers = Parse(engineSchematic, NumberRegex());

        return partNumbers
            .Where(part => symbols.Any(symbol => NextTo(symbol, part)))
            .Select(part => part.PartNumber)
            .Sum();
    }

    private int Part2(string[] engineSchematic)
    {
        var gears = Parse(engineSchematic, GearRegex());
        var partNumbers = Parse(engineSchematic, NumberRegex());

        return gears.Select(gear => new
            {
                gear,
                neighbors = partNumbers.Where(neighbor => NextTo(neighbor, gear)).Select(part => part.PartNumber)
            })
            .Where(gear => gear.neighbors.Count() == 2)
            .Select(gear => gear.neighbors.First() * gear.neighbors.Last())
            .Sum();
    }

    private bool NextTo(EnginePart part1, EnginePart part2)
    {
        return Math.Abs(part2.Row - part1.Row) <= 1 &&
            part1.Col <= part2.Col + part2.Text.Length &&
            part2.Col <= part1.Col + part1.Text.Length;
    }

    private EnginePart[] Parse(string[] rows, Regex rx)
    {
        return Enumerable.Range(0, rows.Length)
            .SelectMany(row => rx.Matches(rows[row]).Cast<Match>(),
                (row, match) => new EnginePart(match.Value, row, match.Index))
            .ToArray();
    }

    [GeneratedRegex(@"[^.0-9]")]
    private static partial Regex SymbolRegex();

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();

    [GeneratedRegex(@"\*")]
    private static partial Regex GearRegex();
}

public record EnginePart(string Text, int Row, int Col) {
    public int PartNumber => int.Parse(Text);
}
