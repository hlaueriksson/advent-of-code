using System.Text.RegularExpressions;

var lines = File.ReadLines("input.txt").Select(GetLineData).ToArray();

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var validLines = lines.Where(x => IsValid(x)).ToArray();
  var map = GetMap(validLines);
  foreach (var line in validLines)
  {
    if (IsHorizontal(line))
    {
      var range = Enumerable.Range(Math.Min(line.y1, line.y2), Math.Abs(line.y1 - line.y2) + 1).ToList();
      range.ForEach(y => map[line.x1, y]++);
    }
    else
    {
      var range = Enumerable.Range(Math.Min(line.x1, line.x2), Math.Abs(line.x1 - line.x2) + 1).ToList();
      range.ForEach(x => map[x, line.y1]++);
    }
  }
  return CalculatePoints(map);
  bool IsValid((int x1, int y1, int x2, int y2) line) => IsHorizontal(line) || IsVertical(line);
}

int PartTwo()
{
  var map = GetMap(lines);
  foreach (var line in lines)
  {
    if (IsHorizontal(line))
    {
      var range = Enumerable.Range(Math.Min(line.y1, line.y2), Math.Abs(line.y1 - line.y2) + 1).ToList();
      range.ForEach(y => map[line.x1, y]++);
    }
    else if (IsVertical(line))
    {
      var range = Enumerable.Range(Math.Min(line.x1, line.x2), Math.Abs(line.x1 - line.x2) + 1).ToList();
      range.ForEach(x => map[x, line.y1]++);
    }
    else
    {
      var length = Math.Abs(line.x1 - line.x2) + 1;
      for (var i = 0; i < length; i++)
      {
        var x = line.x1 < line.x2 ? line.x1 + i : line.x1 - i;
        var y = line.y1 < line.y2 ? line.y1 + i : line.y1 - i;
        map[x, y]++;
      }
    }
  }
  return CalculatePoints(map);
}

(int x1, int y1, int x2, int y2) GetLineData(string line)
{
  var regex = new Regex(@"^(\d{1,3}),(\d{1,3}) -> (\d{1,3}),(\d{1,3})$", RegexOptions.Compiled);
  var matches = regex.Matches(line);
  var groups = matches.First().Groups;
  return (Convert.ToInt32(groups[1].Value), Convert.ToInt32(groups[2].Value), Convert.ToInt32(groups[3].Value), Convert.ToInt32(groups[4].Value));
}

int[,] GetMap((int x1, int y1, int x2, int y2)[] lines)
{
  var size = Math.Max(
    Math.Max(lines.MaxBy(x => x.x1).x1, lines.MaxBy(x => x.y1).y1),
    Math.Max(lines.MaxBy(x => x.x2).x2, lines.MaxBy(x => x.y2).y2)
  ) + 1;
  return new int[size, size];
}

bool IsHorizontal((int x1, int y1, int x2, int y2) line) => line.x1 == line.x2;
bool IsVertical((int x1, int y1, int x2, int y2) line) => line.y1 == line.y2;

int CalculatePoints(int[,] map)
{
  var points = 0;
  for (int y = 0; y < map.GetLength(1); y++)
  {
    for (int x = 0; x < map.GetLength(0); x++)
    {
      if (map[x, y] > 1) points++;
    }
  }
  return points;
}