using System.Text.RegularExpressions;
using Microsoft.Z3;
using static aoc2025.util.Combinations;

namespace aoc2025.solutions
{
    internal class Day10
    {
        public static string[] Solve(string[] input)
        {
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
                        if (TrySequence(machine, combo))
                        {
                            return combo.Count;
                        }
                    }
                }
                return 0;
            }

            static bool TrySequence(Machine machine, List<int> seq)
            {
                machine.Reset();
                foreach (int button in seq)
                {
                    machine.Button(button);
                }
                if (Check(machine))
                {
                    return true;
                }
                return false;
            }

            static bool Check(Machine machine)
            {
                for (int i = 0; i < machine.lights.Length; i++)
                {
                    if (machine.lights[i] != machine.lightgoal[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            public int Part2()
            {
                int sum = 0;
                foreach (var machine in machines)
                {
                    sum += FindJoltSequence(machine);
                }
                return sum;
            }

            static int FindJoltSequence(Machine machine)
            {
                // using Microsoft.Z3
                Context ctx = new();
                Optimize opt = ctx.MkOptimize();

                IntExpr[] buttonconstants = new IntExpr[machine.buttons.Count];

                // (declare - fun buttonX() Int)
                // buttons exist
                // (assert (>= buttonX 0))
                // buttons must be pressed 0 or more times - no negative presses
                for (int b = 0; b < buttonconstants.Length; b++)
                {
                    string name = "button" + b;
                    IntExpr constant = ctx.MkIntConst(name);
                    buttonconstants[b] = constant;
                    IntExpr zero = ctx.MkInt(0);
                    BoolExpr greaterequal = ctx.MkGe(constant, zero);
                    opt.Add(greaterequal);
                }

                // (assert (= (+ buttonX buttonY ...) goal))
                // reach joltage counters' goal values by pressing the buttons that can affect them
                for (int j = 0; j < machine.joltagegoal.Length; j++)
                {
                    List<ArithExpr> relevantbuttons = [];
                    for (var b = 0; b < buttonconstants.Length; b++)
                    {
                        if (machine.buttons[b].Contains(j))
                        {
                            relevantbuttons.Add(buttonconstants[b]);
                        }

                    }
                    ArithExpr buttonsum = ctx.MkAdd(relevantbuttons);
                    IntExpr goal = ctx.MkInt(machine.joltagegoal[j]);
                    BoolExpr equal = ctx.MkEq(buttonsum, goal);
                    opt.Add(equal);
                }

                // (minimize(+buttonX buttonY ...))
                // solving for fewest number of presses
                ArithExpr presses = ctx.MkAdd(buttonconstants);
                opt.MkMinimize(presses);

                // check for optimal values - solve the problem
                opt.Check();

                // get the number of times each button was pressed
                int minpresses = 0;
                foreach (IntExpr button in buttonconstants)
                {
                    IntNum eval = (IntNum)opt.Model.Eval(button);
                    int integer = eval.Int;
                    minpresses += integer;
                }
                return minpresses;
            }

            class Machine
            {
                public readonly bool[] lights;
                public readonly bool[] lightgoal;
                public readonly List<int[]> buttons = [];
                public readonly int[] joltagegoal;

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
                    joltagegoal = joltintarray;
                }

                public void Button(int i)
                {
                    foreach (int light in buttons[i])
                    {
                        lights[light] = !lights[light];
                    }
                }

                public void Reset()
                {
                    Array.Fill(lights, false);
                }
            }
        }
    }
}