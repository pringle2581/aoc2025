using aoc2025.solutions;

string input = "../../../input/";
string[] results = [];
bool complete = false;
while (!complete)
{
    Console.WriteLine("Select a day");
    string? daystring = Console.ReadLine();
    int day = int.TryParse(daystring, out int i) ? i : 0;
    switch (day)
    {
        case 1:
            results = Day01.Solve(File.ReadAllLines(input + "01"));
            break;
        case 2:
            results = Day02.Solve(File.ReadAllLines(input + "02"));
            break;
        //case 3:
        //    results = Day03.Solve(File.ReadAllLines(input + "03"));
        //    break;
        //case 4:
        //    results = Day04.Solve(File.ReadAllLines(input + "04"));
        //    break;
        //case 5:
        //    results = Day05.Solve(File.ReadAllLines(input + "05"));
        //    break;
        //case 6:
        //    results = Day06.Solve(File.ReadAllLines(input + "06"));
        //    break;
        //case 7:
        //    results = Day07.Solve(File.ReadAllLines(input + "07"));
        //    break;
        //case 8:
        //    results = Day08.Solve(File.ReadAllLines(input + "08"));
        //    break;
        //case 9:
        //    results = Day09.Solve(File.ReadAllLines(input + "09"));
        //    break;
        //case 10:
        //    results = Day10.Solve(File.ReadAllLines(input + "10"));
        //    break;
        //case 11:
        //    results = Day11.Solve(File.ReadAllLines(input + "11"));
        //    break;
        //case 12:
        //    results = Day12.Solve(File.ReadAllLines(input + "12"));
        //    break;
        default:
            Console.WriteLine("Invalid day, try again\n");
            break;
    }
    if (results.Length == 2)
    {
        complete = true;
    }
}

Console.WriteLine($"Part 1: {results[0]}\nPart 2: {results[1]}");