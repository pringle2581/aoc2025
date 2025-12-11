namespace aoc2025.solutions
{
    internal class Day11
    {
        public static string[] Solve(string[] input)
        {
            Rack rack = new(input);
            return [rack.Part1().ToString(), rack.Part2().ToString()];
        }

        class Rack
        {
            readonly Dictionary<string, string[]> graph = [];

            public Rack(string[] input)
            {
                foreach (string line in input)
                {
                    string[] split = line.Split(": ");
                    string start = split[0];
                    string[] neighbors = split[1].Split(" ");
                    graph[start] = neighbors;
                }
            }

            public long Part1()
            {
                return Paths(graph, "you", "out", []);
            }

            public long Part2()
            {
                long fftdac = Paths(graph, "fft", "dac", []);
                long dacfft = Paths(graph, "dac", "fft", []);
                if (fftdac > dacfft)
                {
                    long svrfft = Paths(graph, "svr", "fft", []);
                    long dacout = Paths(graph, "dac", "out", []);
                    return svrfft * fftdac * dacout;
                }
                else
                {
                    long svrdac = Paths(graph, "svr", "dac", []);
                    long fftout = Paths(graph, "fft", "out", []);
                    return svrdac * dacfft * fftout;
                }
            }

            static long Paths(Dictionary<string, string[]> graph, string start, string end, Dictionary<string, long> cache)
            {
                if (!cache.ContainsKey(start))
                {
                    if (start == end)
                    {
                        cache[start] = 1;
                    }
                    else
                    {
                        long paths = 0;
                        foreach (string neighbor in graph.GetValueOrDefault(start, []))
                        {
                            paths += Paths(graph, neighbor, end, cache);
                        }
                        cache[start] = paths;
                    }
                }
                return cache[start];
            }
        }
    }
}