namespace aoc2025.util
{
    public class Combinations
    {
        public static List<List<int>> GetCombinations(int n, int k)
        {
            List<List<int>> res = [];
            List<int> temp = [];
            CombineUtil(res, temp, n, 1, k);
            return res;
        }

        static void CombineUtil(List<List<int>> res, List<int> temp, int n, int start, int k)
        {
            if (k == 0)
            {
                res.Add(new List<int>(temp));
                return;
            }
            for (int i = start; i <= n; ++i)
            {
                temp.Add(i);
                CombineUtil(res, temp, n, i + 1, k - 1);
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}
