var crabs = File.ReadAllLines("input.txt").First().Split(',').Select(x => Convert.ToInt32(x)).ToList();
var max = crabs.Max();

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var fuel = int.MaxValue;
  for (int i = 0; i < max; i++)
  {
    var result = 0;
    foreach (var crab in crabs)
    {
      result += Math.Abs(i - crab);
    }
    if (result < fuel) fuel = result;
  }
  return fuel;
}

int PartTwo()
{
  var fuel = int.MaxValue;
  for (int i = 0; i < max; i++)
  {
    var result = 0;
    foreach (var crab in crabs)
    {
      result += GetCost(Math.Abs(i - crab));
    }
    if (result < fuel) fuel = result;
  }
  return fuel;
  int GetCost(int steps)
  {
    var result = 0;
    for (int i = 0; i <= steps; i++)
    {
      result += i;
    }
    return result;
  }
}