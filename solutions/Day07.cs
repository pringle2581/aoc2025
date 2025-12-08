namespace aoc2025.solutions
{
    internal class Day07
    {
        static public string[] Solve(string[] input)
        {
            (int, long) results = Manifold(input);
            return [results.Item1.ToString(), results.Item2.ToString()];
        }

        private static (int, long) Manifold(string[] input)
        {
            int splits = 0;
            long[] beams = new long[input[0].Length];
            for (int y = 0; y < input.Length; y++)
            {
                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x] == 'S')
                    {
                        beams[x] = 1;
                    }
                    if (input[y][x] == '^' && beams[x] > 0)
                    {
                        beams[x - 1] += beams[x];
                        beams[x + 1] += beams[x];
                        beams[x] = 0;
                        splits++;
                    }
                }
            }
            return (splits, beams.Sum());
        }
    }
}
