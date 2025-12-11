using static aoc2025.util.Combinations;

namespace aoc2025.solutions
{
    internal class Day09
    {
        public static string[] Solve(string[] input)
        {
            Floor floor = new(input);
            var (part1, part2) = floor.Results();
            return [part1.ToString(), part2.ToString()];
        }

        class Floor
        {
            readonly List<(long, long)> reds = [];
            readonly HashSet<(long xmin, long xmax, long ymin, long ymax)> greens = [];
            readonly List<((long, long)[] corners, long area)> rectangles = [];

            public Floor(string[] input)
            {
                foreach (var line in input)
                {
                    long[] tile = Array.ConvertAll(line.Split(","), long.Parse);
                    reds.Add((tile[0], tile[1]));
                }

                for (int i = 1; i < reds.Count; i++)
                {
                    greens.Add(Ranges(reds[i - 1], reds[i]));
                }
                greens.Add(Ranges(reds[0], reds[^1]));

                foreach (var combo in GetCombinations(reds.Count, 2))
                {
                    (long,long)[] corners = [reds[combo[0] - 1], reds[combo[1] - 1]];
                    rectangles.Add(((corners), Area(corners)));
                }
                rectangles.Sort((a, b) => b.area.CompareTo(a.area));
            }

            public (long part1, long part2) Results()
            {
                long part1 = rectangles[0].area;
                long part2 = 0;
                foreach (var area in rectangles)
                {
                    if (TestSquare(area.corners))
                    {
                        part2 = area.area;
                        break;
                    }
                }
                return (part1, part2);
            }

            static (long xmin, long xmax, long ymin, long ymax) Ranges((long x, long y) t1, (long x, long y) t2)
            {
                long xmin = Math.Min(t1.x, t2.x);
                long xmax = Math.Max(t1.x, t2.x);
                long ymin = Math.Min(t1.y, t2.y);
                long ymax = Math.Max(t1.y, t2.y);
                return (xmin, xmax, ymin, ymax);
            }

            bool TestSquare((long x, long y)[] c)
            {
                foreach (var green in greens)
                {
                    var (xmin, xmax, ymin, ymax) = Ranges(c[0], c[1]);
                    if (xmin < green.xmax && xmax > green.xmin && ymin < green.ymax && ymax > green.ymin)
                    {
                        return false;
                    }
                }
                return true;
            }

            static long Area((long x, long y)[] c)
            {
                long x = Math.Abs(c[0].x - c[1].x) + 1;
                long y = Math.Abs(c[0].y - c[1].y) + 1;
                return x * y;
            }
        }
    }
}
