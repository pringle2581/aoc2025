namespace aoc2025.solutions
{
    internal class Day12
    {
        public static string[] Solve(string[] input)
        {
            return [Farm.FitGifts(input).ToString(), "<3 done <3"];
        }

        class Farm
        {
            public static int FitGifts(string[] input)
            {
                int sum = 0;
                foreach (string line in input)
                {
                    if (line.Contains('x'))
                    {
                        string[] split = line.Split(": ");
                        string[] sizestr = split[0].Split("x");
                        string[] giftstr = split[1].Split(" ");
                        int[] size = Array.ConvertAll(sizestr, int.Parse);
                        int[] gifts = Array.ConvertAll(giftstr, int.Parse);
                        if (size[0] * size[1] >= gifts.Sum() * 9)
                        {
                            sum++;
                        }
                    }
                }
                return sum;
            }
        }
    }
}
