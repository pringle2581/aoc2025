namespace aoc2025.solutions
{
    internal class Day05
    {
        static public string[] Solve(string[] input)
        {
            InvDB inventory = new(input);
            int part1 = inventory.CheckFreshness();
            long part2 = inventory.GetFreshIDs();
            return [part1.ToString(), part2.ToString()];
        }

        private class InvDB
        {
            class IntervalList : List<(long start, long end)> { }

            readonly IntervalList freshintervals = [];
            readonly HashSet<long> available = [];

            public InvDB(string[] input)
            {
                IntervalList intervals = [];
                foreach (string line in input)
                {
                    if (line.Contains('-'))
                    {
                        long[] interval = Array.ConvertAll(line.Split("-"), long.Parse);
                        intervals.Add((interval[0], interval[1]));
                    }
                    else if (long.TryParse(line, out long ingredient))
                    {
                        available.Add(ingredient);
                    }
                }
                freshintervals = MergeIntervals(intervals);
                return;
            }

            static IntervalList MergeIntervals(IntervalList intervals)
            {
                intervals.Sort();
                IntervalList merged = [];
                foreach (var interval in intervals)
                {

                    if (merged.Count == 0 || merged[^1].end < interval.start)
                    {
                        merged.Add(interval);
                    }
                    else
                    {
                        merged[^1] = (merged[^1].start, Math.Max(merged[^1].end, interval.end));
                    }
                }
                return merged;
            }

            public int CheckFreshness()
            {
                int freshcount = 0;
                foreach (long ingredient in available)
                {
                    foreach (var (start, end) in freshintervals)
                    {
                        if (ingredient >= start && ingredient <= end)
                        {
                            freshcount++;
                            break;
                        }
                    }
                }
                return freshcount;
            }

            public long GetFreshIDs()
            {
                long idcount = 0;
                foreach (var (start, end) in freshintervals)
                {
                    idcount += end - start + 1;
                }
                return idcount;
            }
        }
    }
}
