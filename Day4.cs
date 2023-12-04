namespace Advent_of_Code_2023;

public partial class Day4 : IDay
{
    private readonly string _inputFile = "Input/Day_4_Input.txt";
    private readonly int _partOneAnswer;
    private readonly int _partTwoAnswer;
    private readonly List<Card> _cards;

    public Day4()
    {
        var input = File.ReadAllLines(_inputFile);

        _cards = [];
        foreach (var line in input)
        {
            _cards.Add(new Card(line));
        }

        _partOneAnswer = Part1();
        //_partTwoAnswer = Part2();
    }

    public void Run()
    {
        Console.WriteLine($"Part 1 - The sum of all the points from the cards is: {_partOneAnswer}");
    }

    private int Part1()
    {
        foreach (var card in _cards)
        {
            foreach (var number in card.Numbers)
            {
                if (card.WinningNumbers.Contains(number))
                {
                    card.Points = card.Points == 0 ? 1 : card.Points * 2;
                }
            }
        }

        return _cards.Select(card => card.Points).Sum();
    }

    // private int Part2()
    // {

    // }
}

public class Card
{
    public string CardId { get; set; }
    public int[] WinningNumbers { get; set; }
    public int[] Numbers { get; set; }
    public int Points { get; set; }

    private static readonly char[] separator = [':', '|'];

    public Card(string input)
    {
        var parts = input.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        CardId = parts[0].Trim();
        WinningNumbers = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
        Numbers = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
        Points = 0;
    }
}