namespace aoc2025.solutions
{
    internal class Day03
    {
        static public string[] Solve(string[] input)
        {
            List<int[]> banks = Parse(input);
            long part1 = GetTotalJoltage(banks, 2);
            long part2 = GetTotalJoltage(banks, 12);
            return [part1.ToString(), part2.ToString()];

            List<int[]> Parse(string[] input)
            {
                List<int[]> list = [];
                foreach (string line in input)
                {
                    char[] chararray = line.ToCharArray();
                    int[] numarray = Array.ConvertAll(chararray, num => (int)Char.GetNumericValue(num));
                    list.Add(numarray);
                }
                return list;
            }

            long GetTotalJoltage(List<int[]> banks, int battcount)
            {
                long totaljolt = 0;
                foreach (int[] bank in banks)
                {
                    long maxjolt = 0;
                    int leftbound = 0;
                    for (int currentbatt = 1; currentbatt <= battcount; currentbatt++)
                    {
                        int highbatt = leftbound;
                        for (int i = leftbound + 1 ; i < bank.Length - battcount + currentbatt; i++)
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
