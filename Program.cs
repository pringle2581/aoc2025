using aoc2025.solutions;

Console.WriteLine(@"
    *       Advent
   /.\
  /o..\       of
  /..o\
 /.o..o\     Code
 /...o.\
/..o....\    2025
^^^[_]^^^
");

while (true)
{
    Console.WriteLine("Select a day");
    string? daystring = Console.ReadLine();
    int day = int.TryParse(daystring, out int i) ? i : 0;
    string[] results = SolveDay(day);
    if (results.Length == 2)
    {
        Console.WriteLine($"Part 1: {results[0]}\nPart 2: {results[1]}");
        break;
    }
}

static string[] SolveDay(int day)
{
    string input = "../../../input/";
    return day switch
    {
        1 => Day01.Solve(File.ReadAllLines(input + "01")),
        2 => Day02.Solve(File.ReadAllLines(input + "02")),
        3 => Day03.Solve(File.ReadAllLines(input + "03")),
        4 => Day04.Solve(File.ReadAllLines(input + "04")),
        5 => Day05.Solve(File.ReadAllLines(input + "05")),
        6 => Day06.Solve(File.ReadAllLines(input + "06")),
        7 => Day07.Solve(File.ReadAllLines(input + "07")),
        8 => Day08.Solve(File.ReadAllLines(input + "08")),
        9 => Day09.Solve(File.ReadAllLines(input + "09")),
        10 => Day10.Solve(File.ReadAllLines(input + "10")),
        11 => Day11.Solve(File.ReadAllLines(input + "11")),
        //12 => Day12.Solve(File.ReadAllLines(input + "12")),
        _ => []
    };
}