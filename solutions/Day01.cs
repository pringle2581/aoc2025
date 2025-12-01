namespace aoc2025.solutions
{
    internal class Day01
    {
        static public string[] Solve(string[] input)
        {
            int part1 = 0, part2 = 0;
            List<int> rotations = Parse(input);
            Solve(rotations);
            return [part1.ToString(), part2.ToString()];

            List<int> Parse(string[] input)
            {
                List<int> list = [];
                foreach (string line in input)
                {
                    int num = int.Parse(line[1..]);
                    if (line[..1] == "L")
                    {
                        num *= -1;
                    }
                    list.Add(num);
                }
                return list;
            }

            void Solve(List<int> steps)
            {
                int dial = 50;
                foreach (var step in steps)
                {
                    dial = Turn(dial, step);
                    if (dial == 0) {
                        part1++;
                    }
                }
            }

            int Turn(int dial, int step)
            {
                for (int i = 0; i < Math.Abs(step); i++)
                {
                    dial = dial + Math.Sign(step);
                    if (dial == -1) { dial = 99; }
                    if (dial == 100) { dial = 0; }
                    if (dial == 0) { part2++; }
                }
                return dial;
            }
        }
    }
}
