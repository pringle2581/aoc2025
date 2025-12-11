using System.Text.RegularExpressions;
using static aoc2025.util.Combinations;

namespace aoc2025.solutions
{
    internal class Day10
    {
        public static string[] Solve(string[] input) {
            string[] sample = "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}\r\n[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}\r\n[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}".Split("\r\n");
            Factory factory = new(input);
            return [factory.Part1().ToString(), factory.Part2().ToString()];
        }

        class Factory
        {
            readonly List<Machine> machines = [];

            public Factory(string[] input)
            {
                foreach (string line in input)
                {
                    machines.Add(new(line));
                }
            }

            public int Part1()
            {
                int sum = 0;
                foreach (var machine in machines)
                {
                    sum += FindSequence(machine);
                }
                return sum;
            }

            public int Part2()
            {
                // idk
                return 0;
            }

            static int FindSequence(Machine machine)
            {
                int buttoncount = machine.buttons.Count;
                for (int digits = 1; digits <= buttoncount; digits++)
                {
                    List<List<int>> combos = GetCombinations(buttoncount, digits);
                    foreach (List<int> combo in combos)
                    {
                        for (int c = 0; c < combo.Count; c++)
                        {
                            combo[c]--;
                        }
                        if (machine.TryLightSequence(combo))
                        {
                            return combo.Count;
                        }
                    }
                }
                return 0;
            }
        }

        class Machine
        {
            readonly bool[] lights;
            readonly bool[] lightgoal;
            readonly public List<int[]> buttons = [];
            readonly int[] joltages;
            readonly int[] joltagegoal;

            public Machine(string line)
            {
                string ptrnlights = @"[\.|#]+";
                string ptrnnums = @"[\d|,]+";
                string strlights = Regex.Match(line, ptrnlights).Value;
                MatchCollection intmatches = Regex.Matches(line, ptrnnums);

                lights = new bool[strlights.Length];
                lightgoal = new bool[strlights.Length];
                for (int i = 0; i < strlights.Length; i++)
                {
                    if (strlights[i] == '#')
                    {
                        lightgoal[i] = true;
                    }
                }

                for (int i = 0; i < intmatches.Count - 1; i++)
                {
                    string[] strarray = intmatches[i].Value.Split(",");
                    int[] intarray = Array.ConvertAll(strarray, int.Parse);
                    buttons.Add(intarray);
                }

                string[] joltstrarray = intmatches[^1].Value.Split(",");
                int[] joltintarray = Array.ConvertAll(joltstrarray, int.Parse);
                joltages = new int[joltintarray.Length];
                joltagegoal = joltintarray;
            }

            public bool TryLightSequence(List<int> seq)
            {
                Array.Fill(lights, false);
                foreach (int button in seq)
                {
                    LightButton(button);
                }
                if (LightCheck())
                {
                    return true;
                }
                return false;
            }

            public bool TryJoltSequence(List<int> seq)
            {
                Array.Fill(joltages, 0);
                foreach (int button in seq)
                {
                    JoltButton(button);
                }
                if (JoltCheck())
                {
                    return true;
                }
                return false;
            }

            bool LightCheck()
            {
                for (int i = 0; i < lights.Length; i++)
                {
                    if (lights[i] != lightgoal[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            bool JoltCheck()
            {
                for (int i = 0; i < joltages.Length; i++)
                {
                    if (joltages[i] != joltagegoal[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public void LightButton(int i)
            {
                foreach (int light in buttons[i])
                {
                    lights[light] = !lights[light];
                }
            }

            public void JoltButton(int i)
            {
                foreach (int counter in buttons[i])
                {
                    joltages[counter]++;
                }
            }
        }
    }
}
