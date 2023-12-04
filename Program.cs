using System.Globalization;
using System.Text;

namespace Advent_of_Code_2023;

class Program
{
    private const int _days = 3;
	private const string _heading = "Advent of Code - 2023";
	private const int _headerWidth = 45;
	private static readonly int _whiteSpaceLength =	(_headerWidth - _heading.Length - 4) / 2;
	private static bool _runProgram = true;
	private static bool _validMenuSelection = true;

	private static void Main()
	{
		while (_runProgram)
		{
			Console.Write(GetMenu());
			var menuInput = Console.ReadLine()?.ToUpperInvariant();
			if (menuInput == "Q" || menuInput == "QUIT")
			{
				_runProgram = false;
				continue;
			}

			_validMenuSelection = int.TryParse(menuInput, out var menuSelection)
				&& menuSelection > 0
				&& menuSelection <= (_days + 2);
			if(!_validMenuSelection)
			{
				Console.Clear();
				continue;
			}
			else
			{
				Console.Clear();

				var days = new List<IDay>() {
					new Day1(),
					new Day2(),
					new Day3(),
				};
				var runAll = menuSelection == _days + 1;

				if (runAll)
				{
					Console.WriteLine("Day 1\n-----\n");
					days[0].Run();
					for (var day = 2; day <= _days; day++)
					{
						Console.WriteLine($"\nDay {day}\n" +
							$"{new string('-', 4 + day.ToString(CultureInfo.InvariantCulture).Length)}\n");
						days[day - 1].Run();
					}
				}
				else
				{
					Console.WriteLine($"Day {menuSelection}\n" +
						$"{new string('-', 4 + menuSelection.ToString(CultureInfo.InvariantCulture).Length)}\n");
					days[menuSelection - 1].Run();
				}

				_runProgram = RunAgain();

				Console.Clear();
			}
		}

		Console.Clear();
		Console.WriteLine("Good bye.  Hope you enjoyed the Advent of Code - 2023.");
	}

	private static string GetMenu()
	{
		StringBuilder sb = new();

		// Header
		sb.AppendLine(new string('*', _headerWidth));
		sb.Append("**");
		sb.Append(new string(' ', _whiteSpaceLength));
		sb.Append(_heading);
		sb.Append(new string(' ', _whiteSpaceLength));
		sb.Append("**\n");
		sb.AppendLine(new string('*', _headerWidth));
		sb.AppendLine();

		// Menu
		sb.AppendLine("Select a day to run:");
		sb.AppendLine(new string('-', 20));
		for(int day = 1; day <= _days; day++)
		{
			var line = _days < 10 ?
				$"{day}) Day {day}" :
				$"{string.Format(CultureInfo.CurrentCulture, "{0,2:##}", day)}) Day {day}";
			sb.AppendLine(line);
		}
		sb.AppendLine(CultureInfo.CurrentCulture, $"{_days + 1}) All Days\n");
		sb.AppendLine($"Q) Quit");
		sb.AppendLine(new string('-', 20));

		// Display if previous input is invalid
		if (!_validMenuSelection)
		{
			sb.AppendLine("Invalid input.  Please try again.");
		}

		// Input prompt
		sb.Append("=> ");

		return sb.ToString();
	}

	public static bool RunAgain()
	{
		Console.Write("\nRun again? (y/n) => ");
		var keepRunningInput = Console.ReadLine()?.ToUpperInvariant();

		var keepRunning = true;
		if (keepRunningInput == "N" || keepRunningInput == "NO")
		{
			keepRunning = false;
		}
		else if (!(keepRunningInput == "Y" || keepRunningInput == "YES"))
		{
			_validMenuSelection = false;
		}

		return keepRunning;
	}
}
