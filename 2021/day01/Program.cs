var depths = File.ReadAllLines("input.txt").Select(x => Convert.ToInt32(x)).ToList();
Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var previous = 0;
  var increases = 0;
  foreach (var d in depths)
  {
    if (d > previous) increases++;
    previous = d;
  }
  return increases - 1;
}

int PartTwo()
{
  var previous = 0;
  var increases = 0;
  for (int i = 0; i < depths.Count() - 2; i++)
  {
    var d = depths[i] + depths[i + 1] + depths[i + 2];
    if (d > previous) increases++;
    previous = d;
  }
  return increases - 1;
}
