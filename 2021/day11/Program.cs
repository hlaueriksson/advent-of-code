var input = File.ReadLines("input.txt");
int[][] octopuses;

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  octopuses = input.Select(x => x.Select(y => y - '0').ToArray()).ToArray();
  return Run(100).flashes;
}

int PartTwo()
{
  octopuses = input.Select(x => x.Select(y => y - '0').ToArray()).ToArray();
  return Run(1000).step;
}

(int flashes, int step) Run(int steps)
{
  var flashes = 0;
  var synchronized = 0;
  for (int step = 1; step <= steps; step++)
  {
    var next = new (int level, bool flashed)[octopuses.Length][];
    CopyToNext(octopuses, next);
    for (int y = 0; y < octopuses.Length; y++)
    {
      for (int x = 0; x < octopuses[0].Length; x++)
      {
        next[y][x].level++;
      }
    }
    (int y, int x, int level, bool flashed)[] flashers;
    while ((flashers = next.SelectMany((array, y) => array.Select((octopus, x) => (y, x, octopus.level, octopus.flashed))).Where(octopus => !octopus.flashed && octopus.level > 9).ToArray()).Any())
    {
      foreach (var f in flashers)
      {
        var neighbors = GetNeighbors(f.y, f.x);
        foreach (var n in neighbors)
        {
          if (!next[n.y][n.x].flashed) next[n.y][n.x].level++;
        }
        next[f.y][f.x].flashed = true;
        next[f.y][f.x].level = 0;
        flashes++;
      }
    }
    CopyFromNext(next, octopuses);
    if (next.All(array => array.All(octopus => octopus.flashed == true)))
    {
      synchronized = step;
      break;
    }
  }
  return (flashes, synchronized);
}

void CopyToNext(int[][] from, (int level, bool flashed)[][] to)
{
  for (int y = 0; y < from.Length; y++)
  {
    to[y] = new (int level, bool flashed)[from[y].Length];
    for (int x = 0; x < from[0].Length; x++)
    {
      to[y][x].level = from[y][x];
    }
  }
}

void CopyFromNext((int level, bool flashed)[][] from, int[][] to)
{
  for (int y = 0; y < from.Length; y++)
  {
    for (int x = 0; x < from[0].Length; x++)
    {
      to[y][x] = from[y][x].level;
    }
  }
}

IEnumerable<(int y, int x)> GetNeighbors(int y, int x)
{
  if (y > 0) yield return (y - 1, x);
  if (x > 0) yield return (y, x - 1);
  if (y < octopuses.Length - 1) yield return (y + 1, x);
  if (x < octopuses[y].Length - 1) yield return (y, x + 1);
  if (y > 0 && x > 0) yield return (y - 1, x - 1);
  if (y > 0 && x < octopuses[y].Length - 1) yield return (y - 1, x + 1);
  if (y < octopuses.Length - 1 && x < octopuses[y].Length - 1) yield return (y + 1, x + 1);
  if (y < octopuses.Length - 1 && x > 0) yield return (y + 1, x - 1);
}