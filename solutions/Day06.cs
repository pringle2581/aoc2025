namespace aoc2025.solutions
{
    internal class Day06
    {
        static public string[] Solve(string[] input)
        {
            long part1 = Worksheet.Part1(input);
            long part2 = Worksheet.Part2(input);
            return [part1.ToString(), part2.ToString()];
        }

        class Worksheet
        {
            class Problem()
            {
                public List<long> numbers = [];
                public bool mult = false;
                public long Solve()
                {
                    if (mult)
                    {
                        return numbers.Aggregate((a, x) => a * x);
                    }
                    else
                    {
                        return numbers.Aggregate((a, x) => a + x);
                    }
                }
            }

            public static long Part1(string[] input)
            {
                long total = 0;
                List<string[]> lines = [];
                foreach (string line in input)
                {
                    lines.Add(line.Split().Where(x => !string.IsNullOrEmpty(x)).ToArray());
                }
                for (int i = 0; i < lines[0].Length; i++)
                {
                    Problem problem = new();
                    for (int line = 0; line < lines.Count - 1; line++)
                    {
                        problem.numbers.Add(long.Parse(lines[line][i]));
                    }
                    if (lines[^1][i] == "*")
                    {
                        problem.mult = true;
                    }
                    total += problem.Solve();
                }
                return total;
            }

            public static long Part2(string[] input)
            {
                long total = 0;
                Problem problem = new();
                for (int i = 0; i < input[0].Length; i++)
                {
                    string slice = "";
                    for (int line = 0; line < input.Length; line++)
                    {
                        slice += input[line][i];
                    }
                    if (slice != new string(' ', input.Length))
                    {
                        if (slice.Contains('*'))
                        {
                            problem.mult = true;
                        }
                        problem.numbers.Add(long.Parse(slice[..^1]));
                    }
                    else
                    {
                        total += problem.Solve();
                        problem = new();
                    }
                }
                total += problem.Solve();
                return total;
            }
        }
    }
}
