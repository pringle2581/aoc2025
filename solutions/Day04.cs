namespace aoc2025.solutions
{
    internal class Day04
    {
        static public string[] Solve(string[] input)
        {
            Rolls rolls = new();
            rolls.Parse(input);
            int part1 = rolls.FindAccessible().Count;
            int part2 = rolls.Remove();
            return [part1.ToString(), part2.ToString()];
        }

        private class Rolls
        {
            readonly HashSet<(int, int)> rolls = [];

            public void Parse(string[] input)
            {
                for (int x = 0; x < input.Length; x++)
                {
                    for (int y = 0; y < input[0].Length; y++)
                    {
                        if (input[x][y] == '@')
                        {
                            rolls.Add((x, y));
                        }
                    }
                }
                return;
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

            public HashSet<(int, int)> FindAccessible()
            {
                HashSet<(int, int)> accessible = [];
                foreach ((int, int) roll in rolls)
                {
                    if (CheckAccess(roll))
                    {
                        accessible.Add(roll);
                    }
                }
                return accessible;
            }

            public int Remove()
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
                    else
                    {
                        break;
                    }
                }
                return orig - rolls.Count;
            }
        }
    }
}
