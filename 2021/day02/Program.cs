var commands = File.ReadLines("input.txt").Select(x => { var s = x.Split(' '); return new { Direction = s[0], Unit = Convert.ToInt32(s[1]) }; }).ToList();
Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  int x = 0, y = 0;
  foreach (var command in commands)
  {
    switch (command.Direction)
    {
      case "forward":
        x += command.Unit;
        break;
      case "down":
        y += command.Unit;
        break;
      case "up":
        y -= command.Unit;
        break;
    }
  }
  return x * y;
}

int PartTwo()
{
  int x = 0, y = 0, aim = 0;
  foreach (var command in commands)
  {
    switch (command.Direction)
    {
      case "forward":
        x += command.Unit;
        y += aim * command.Unit;
        break;
      case "down":
        aim += command.Unit;
        break;
      case "up":
        aim -= command.Unit;
        break;
    }
  }
  return x * y;
}
