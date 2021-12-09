var heightmap = File.ReadLines("input.txt").Select(x => x.Select(y => y - '0').ToArray()).ToArray();

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var lowPoints = new List<int>();
  for (int y = 0; y < heightmap.Length; y++)
  {
    for (int x = 0; x < heightmap[y].Length; x++)
    {
      if (GetNeighbors(y, x).All(i => i > heightmap[y][x])) lowPoints.Add(heightmap[y][x]);
    }
  }
  return lowPoints.Sum(i => i + 1);
}

int PartTwo()
{
  var lowPoints = new List<(int y, int x)>();
  for (int y = 0; y < heightmap.Length; y++)
  {
    for (int x = 0; x < heightmap[y].Length; x++)
    {
      if (GetNeighbors(y, x).All(i => i > heightmap[y][x])) lowPoints.Add((y, x));
    }
  }
  var basins = new List<int>();
  bool[,] visited;
  int count;
  foreach (var p in lowPoints)
  {
    visited = new bool[heightmap.Length, heightmap[0].Length];
    count = 0;
    CalculateBasin(p.y, p.x);
    basins.Add(count);
  }
  basins.Sort();
  return basins.TakeLast(3).Aggregate(1, (x, y) => x * y);
  void CalculateBasin(int y, int x)
  {
    if (y < 0 || x < 0 || y >= heightmap.Length || x >= heightmap[0].Length) return;
    if (visited[y, x]) return;
    if (heightmap[y][x] == 9)
    {
      visited[y, x] = true;
      return;
    }
    count++;
    visited[y, x] = true;
    CalculateBasin(y - 1, x);
    CalculateBasin(y + 1, x);
    CalculateBasin(y, x - 1);
    CalculateBasin(y, x + 1);
  }
}

IEnumerable<int> GetNeighbors(int y, int x)
{
  if (y > 0) yield return heightmap[y - 1][x];
  if (x > 0) yield return heightmap[y][x - 1];
  if (y < heightmap.Length - 1) yield return heightmap[y + 1][x];
  if (x < heightmap[y].Length - 1) yield return heightmap[y][x + 1];
}
