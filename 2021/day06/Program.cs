var input = File.ReadAllLines("input.txt")[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();
Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var list = new List<int>(input);
  for (int day = 1; day <= 80; day++)
  {
    var index = 0;
    foreach (var fish in list.ToList())
    {
      if (fish == 0)
      {
        list[index] = 6;
        list.Add(8);
      }
      else
      {
        list[index]--;
      }
      index++;
    }
  }
  return list.Count();
}

long PartTwo()
{
  var data = new long[9];
  foreach (var fish in input)
  {
    data[fish]++;
  }
  for (int day = 1; day <= 256; day++)
  {
    var next = new long[9];
    for (int i = 0; i < 9; i++)
    {
      if (i == 0)
      {
        next[6] = data[i];
        next[8] = data[i];
      }
      else
      {
        next[i - 1] += data[i];
      }
    }
    Array.Copy(next, data, 9);
  }
  return data.Sum(x => x);
}