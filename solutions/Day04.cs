namespace aoc2025.solutions
{
    internal class Day04
    {
        static public string[] Solve(string[] input)
        {
            HashSet<(int, int)> rolls = Parse(input);
            int part1 = FindAccessible().Count;
            int part2 = Remove();
            return [part1.ToString(), part2.ToString()];

            HashSet<(int, int)> Parse(string[] input)
            {
                HashSet<(int, int)> grid = [];
                for (int x = 0; x < input.Length; x++)
                {
                    for (int y = 0; y < input[0].Length; y++)
                    {
                        if (input[x][y] == '@')
                        {
                            grid.Add((x, y));
                        }
                    }
                }
                return grid;
            }

            bool CheckAccess((int x, int y) roll)
            {
                int adj = 0;
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (rolls.Contains((roll.x + x, roll.y + y)))
                        {
                            adj++;
                        }
                    }
                }
                return adj < 5;
            }

            HashSet<(int, int)> FindAccessible()
            {
                HashSet<(int, int)> accessible = [];
                foreach ((int, int) roll in rolls)
                {
                    if (CheckAccess(roll)) {
                        accessible.Add(roll);
                    }
                }
                return accessible;
            }

            int Remove()
            {
                int orig = rolls.Count;
                while (true)
                {
                    HashSet<(int, int)> accessible = FindAccessible();
                    if (accessible.Count > 0)
                    {
                        foreach ((int, int) roll in accessible)
                        {
                            rolls.Remove(roll);
                        }
                    }
                    else {
                        break;
                    }
                }
                return orig - rolls.Count;
            }
        }
    }
}
