namespace aoc2025.solutions
{
    internal class Day03
    {
        static public string[] Solve(string[] input)
        {
            Banks banks = new(input);
            long part1 = banks.GetTotalJoltage(2);
            long part2 = banks.GetTotalJoltage(12);
            return [part1.ToString(), part2.ToString()];
        }

        private class Banks
        {
            readonly List<int[]> banks = [];

            public Banks(string[] input)
            {
                foreach (string line in input)
                {
                    banks.Add(Array.ConvertAll(line.ToCharArray(), num => (int)Char.GetNumericValue(num)));
                }
            }

            public long GetTotalJoltage(int battcount)
            {
                long totaljolt = 0;
                foreach (int[] bank in banks)
                {
                    long maxjolt = 0;
                    int leftbound = 0;
                    for (int currentbatt = 1; currentbatt <= battcount; currentbatt++)
                    {
                        int highbatt = leftbound;
                        for (int i = leftbound + 1; i < bank.Length - battcount + currentbatt; i++)
                        {
                            if (bank[i] > bank[highbatt])
                            {
                                highbatt = i;
                            }
                        }
                        leftbound = highbatt + 1;
                        maxjolt += bank[highbatt] * (long)Math.Pow(10, (battcount - currentbatt));
                    }
                    totaljolt += maxjolt;
                }
                return totaljolt;
            }
        }
    }
}
