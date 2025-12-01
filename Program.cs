using aoc2025.solutions;

bool complete = false;
string input = "../../../input/";
string[] results = [];
while (!complete)
{
    Console.WriteLine("Select a day");
    string? daystring = Console.ReadLine();
    int day = int.TryParse(daystring, out int i) ? i : 0;

    switch (day)
    {
        case 1:
            results = Day01.Solve(File.ReadAllLines(input + "01"));
            complete = true;
            break;
        default:
            Console.WriteLine("Invalid day, try again\n");
            break;
    }
}

Console.WriteLine($"Part 1: {results[0]}\nPart 2: {results[1]}");