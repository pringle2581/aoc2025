namespace aoc2025.solutions
{
    internal class Day01
    {
        static public string[] Solve(string[] input)
        {
            Dial dial = new(input);
            dial.Turn();
            return [dial.part1.ToString(), dial.part2.ToString()];
        }

        private class Dial
        {
            int dial = 50;
            readonly List<int> rotations = [];
            public int part1, part2 = 0;

            public Dial(string[] input)
            {
                foreach (string line in input)
                {
                    int num = int.Parse(line[1..]);
                    if (line[..1] == "L")
                    {
                        num *= -1;
                    }
                    rotations.Add(num);
                }
            }

            public void Turn()
            {
                foreach (int rotation in rotations)
                {
                    for (int i = 0; i < Math.Abs(rotation); i++)
                    {
                        dial = dial + Math.Sign(rotation);
                        if (dial == -1) { dial = 99; }
                        if (dial == 100) { dial = 0; }
                        if (dial == 0) { part2++; }
                    }
                    if (dial == 0)
                    {
                        part1++;
                    }
                }
            }
        }
    }
}
