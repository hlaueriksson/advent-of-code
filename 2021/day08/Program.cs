var input = File.ReadAllLines("input.txt");

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var data = input.Select(GetData).ToArray();
  var uniques = new[] { 7, 4, 3, 2 };
  var result = 0;
  foreach (var d in data)
  {
    result += d.output.Count(x => uniques.Contains(x.Length));
  }
  return result;
}

int PartTwo()
{
  var data = input.Select(GetData).ToArray();
  var result = 0;
  foreach (var d in data)
  {
    var map = d.signal.ToDictionary(x => x, x => 0);
    foreach (var s in d.signal)
    {
      if (s.Length == 2) map[s] = 1;
      else if (s.Length == 3) map[s] = 7;
      else if (s.Length == 4) map[s] = 4;
      else if (s.Length == 5)
      {
        if (KeyOf(1).All(s.Contains)) map[s] = 3;
        else if (KeyOf(4).Count(s.Contains) == 3) map[s] = 5;
        else map[s] = 2;
      }
      else if (s.Length == 6)
      {
        if (KeyOf(4).All(s.Contains)) map[s] = 9;
        else if (KeyOf(7).All(s.Contains)) map[s] = 0;
        else map[s] = 6;
      }
      else if (s.Length == 7) map[s] = 8;
    }
    result += map[d.output[0]] * 1000 + map[d.output[1]] * 100 + map[d.output[2]] * 10 + map[d.output[3]];
    string KeyOf(int value) => map.Single(x => x.Value == value).Key;
  }
  return result;
}

(string[] signal, string[] output) GetData(string line)
{
  var tokens = line.Split(' ').Select(x => string.Concat(x.OrderBy(y => y))).ToList();
  var index = tokens.IndexOf("|");
  return (tokens.Take(index).OrderBy(x => x.Length).ToArray(), tokens.Skip(index + 1).ToArray());
}