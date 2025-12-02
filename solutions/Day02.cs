using System.Text.RegularExpressions;

namespace aoc2025.solutions
{
    internal class Day02
    {
        static public string[] Solve(string[] input)
        {
            long part1 = 0, part2 = 0;
            List<(long, long)> ranges = Parse(input);
            Solve(ranges);
            return [part1.ToString(), part2.ToString()];

            List<(long, long)> Parse(string[] input)
            {
                List<(long, long)> list = [];
                foreach (string line in input[0].Split(","))
                {
                    string[] range = line.Split("-");
                    list.Add((long.Parse(range[0]), long.Parse(range[1])));
                }
                return list;
            }

            void Solve(List<(long start, long end)> ranges)
            {
                foreach (var (start, end) in ranges)
                {
                    for (long id = start; id <= end; id++)
                    {
                        if (!Validate(id))
                        {
                            part2 += id;
                        }
                    }
                }
            }

            bool Validate(long id)
            {
                bool valid = true;
                string idstr = id.ToString();
                for (int slicelength = 1; slicelength <= idstr.Length / 2; slicelength++)
                {
                    if (idstr.Length % slicelength == 0)
                    {
                        HashSet<string> slices = [];
                        for (int slicer = 0; slicer < idstr.Length; slicer += slicelength)
                        {
                            slices.Add(idstr[slicer..(slicer + slicelength)]);
                        }
                        if (slices.Count == 1)
                        {
                            if (idstr.Length / slicelength == 2)
                            {
                                part1 += id;
                            }
                            valid = false;
                        }
                    }
                }
                return valid;
            }
        }
    }
}
