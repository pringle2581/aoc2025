using static aoc2025.util.Combinations;

namespace aoc2025.solutions
{
    internal class Day08
    {
        public static string[] Solve(string[] input)
        {
            Boxes boxes = new(input);
            long[] results = boxes.Solve();
            return [results[0].ToString(), results[1].ToString()];
        }

        class Boxes
        {
            readonly List<int[]> boxes = [];
            readonly List<(int, int, long)> pairs = [];

            public Boxes(string[] input)
            {
                foreach (string line in input)
                {
                    boxes.Add(Array.ConvertAll(line.Split(","), int.Parse));
                }
                GetPairs();
            }

            public long[] Solve()
            {
                long part1 = 0;
                long part2 = 0;
                Dictionary<int, int> circuits = [];
                for (int i = 0; i < boxes.Count; i++)
                {
                    circuits.Add(i, i);
                }
                for (int i = 0; i < pairs.Count; i++)
                {
                    int to = circuits[pairs[i].Item1];
                    int from = circuits[pairs[i].Item2];

                    if (to != from)
                    {
                        foreach (var circuit in circuits.Where(kvp => kvp.Value == from))
                        {
                            circuits[circuit.Key] = circuits[to];
                        }
                    }

                    if (i == 1000 - 1)
                    {
                        int[] counts = new int[boxes.Count];
                        foreach (var circuit in circuits)
                        {
                            counts[circuit.Value]++;
                        }
                        Array.Sort(counts);
                        part1 = counts[^3] * counts[^2] * counts[^1];
                    }

                    if (circuits.Values.Distinct().Count() == 1)
                    {
                        part2 = Math.BigMul(boxes[pairs[i].Item1][0], boxes[pairs[i].Item2][0]);
                        break;
                    }
                }
                return [part1, part2];
            }

            void GetPairs()
            {
                foreach (List<int> combo in GetCombinations(boxes.Count, 2))
                {
                    combo[0]--;
                    combo[1]--;
                    long distance = Distance(boxes[combo[0]], boxes[combo[1]]);
                    pairs.Add((combo[0], combo[1], distance));
                }
                pairs.Sort((x, y) => x.Item3.CompareTo(y.Item3));
            }

            static long Distance(int[] box1, int[] box2)
            {
                long sum = 0;
                for (int i = 0; i <= 2; i++)
                {
                    sum += (long)Math.Pow(box1[i] - box2[i], 2);
                }
                return sum;
            }
        }
    }
}
