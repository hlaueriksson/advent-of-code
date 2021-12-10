var input = File.ReadAllLines("input.txt");
var start = new List<char> { '(', '[', '{', '<' };
var end = new List<char> { ')', ']', '}', '>' };

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var score = 0;
  foreach (var line in input)
  {
    var stack = new Stack<char>();
    foreach (var c in line)
    {
      if (start.Contains(c))
      {
        stack.Push(c);
      }
      else
      {
        var index = end.IndexOf(c);
        if (stack.Pop() != start[index])
        {
          score += GetScore(c);
          break;
        }
      }
    }
  }
  return score;
  int GetScore(char c)
  {
    switch (c)
    {
      case ')': return 3;
      case ']': return 57;
      case '}': return 1197;
      case '>': return 25137;
      default: return 0;
    }
  }
}

long PartTwo()
{
  var scores = new List<long>();
  foreach (var line in input)
  {
    long score = 0;
    var stack = new Stack<char>();
    foreach (var c in line)
    {
      if (start.Contains(c))
      {
        stack.Push(c);
      }
      else
      {
        var index = end.IndexOf(c);
        if (stack.Pop() != start[index])
        {
          stack.Clear();
          break;
        }
      }
    }
    while (stack.Any())
    {
      var index = start.IndexOf(stack.Pop());
      score *= 5;
      score += GetScore(end[index]);
    }
    if (score > 0) scores.Add(score);
  }
  scores.Sort();
  return scores.ElementAt(scores.Count() / 2);
  long GetScore(char c)
  {
    switch (c)
    {
      case ')': return 1;
      case ']': return 2;
      case '}': return 3;
      case '>': return 4;
      default: return 0;
    }
  }
}
