var input = File.ReadLines("input.txt").ToList();
(int x, int y)[] dots = input.Take(input.IndexOf("")).Select(x => { var s = x.Split(','); return (Convert.ToInt32(s[0]), Convert.ToInt32(s[1])); }).ToArray();
(string direction, int line)[] folds = input.Skip(input.IndexOf("") + 1).Select(x => { var s = x.Replace("fold along ", string.Empty).Split('='); return (s[0], Convert.ToInt32(s[1])); }).ToArray();

Console.WriteLine(PartOne());
PartTwo();

int PartOne()
{
  var map = Fold(GetMap(), folds.First());
  return map.Cast<bool>().Count(x => x);
}

void PartTwo()
{
  var map = GetMap();
  foreach (var f in folds)
  {
    map = Fold(map, f);
  }
  // let the mechanical turk figure it out
  Print(map);
}

bool[,] GetMap()
{
  var map = new bool[dots.Max(dot => dot.x) + 1, dots.Max(dot => dot.y) + 1];
  foreach (var d in dots)
  {
    map[d.x, d.y] = true;
  }
  return map;
}

bool[,] Fold(bool[,] map, (string direction, int line) f)
{
  if (f.direction == "y")
  {
    var next = new bool[map.GetLength(0), map.GetLength(1) / 2];
    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        if (!map[x, y]) continue;
        if (y == f.line) continue;
        if (y < f.line)
        {
          next[x, y] = true;
        }
        else
        {
          next[x, f.line * 2 - y] = true;
        }
      }
    }
    return next;
  }
  else
  {
    var next = new bool[map.GetLength(0) / 2, map.GetLength(1)];
    for (int y = 0; y < map.GetLength(1); y++)
    {
      for (int x = 0; x < map.GetLength(0); x++)
      {
        if (!map[x, y]) continue;
        if (x == f.line) continue;
        if (x < f.line)
        {
          next[x, y] = true;
        }
        else
        {
          next[f.line * 2 - x, y] = true;
        }
      }
    }
    return next;
  }
}

void Print(bool[,] map)
{
  for (int y = 0; y < map.GetLength(1); y++)
  {
    for (int x = 0; x < map.GetLength(0); x++)
    {
      Console.Write(map[x, y] ? '#' : '.');
    }
    Console.WriteLine();
  }
}