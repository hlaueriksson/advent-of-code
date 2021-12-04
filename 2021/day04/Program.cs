var input = File.ReadLines("input.txt").ToArray();
Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var numbers = GetNumbers();
  var boards = GetBoards();
  foreach (var n in numbers)
  {
    foreach (var b in boards)
    {
      if (b.ContainsKey(n)) b[n] = (b[n].x, b[n].y, true);
      if (HasBingo(b)) return CalculateScore(b, n);
    }
  }
  return 0;
}

int PartTwo()
{
  var numbers = GetNumbers();
  var boards = GetBoards();
  foreach (var n in numbers)
  {
    foreach (var b in boards.ToList())
    {
      if (b.ContainsKey(n)) b[n] = (b[n].x, b[n].y, true);
      if (HasBingo(b) && boards.Count() > 1) boards.Remove(b);
    }

    if (boards.Count() == 1 && HasBingo(boards.Single())) return CalculateScore(boards.Single(), n);
  }
  return 0;
}

int[] GetNumbers() => input[0].Split(',').Select(x => Convert.ToInt32(x)).ToArray();

List<Dictionary<int, (int x, int y, bool marked)>> GetBoards()
{
  var boards = new List<Dictionary<int, (int x, int y, bool marked)>>();
  var line = 2;
  while (line < input.Count())
  {
    var board = new Dictionary<int, (int x, int y, bool marked)>();
    for (int j = 0; j < 5; j++)
    {
      var row = input[line].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
      for (int k = 0; k < 5; k++)
      {
        board.Add(row[k], (k, j, false));
      }
      line++;
    }
    boards.Add(board);
    line++;
  }
  return boards;
}

bool HasBingo(Dictionary<int, (int x, int y, bool marked)> board)
{
  var kvps = board.Where(x => x.Value.marked);
  return
    kvps.GroupBy(x => x.Value.x).Any(x => x.Count() == 5) ||
    kvps.GroupBy(y => y.Value.y).Any(y => y.Count() == 5);
}

int CalculateScore(Dictionary<int, (int x, int y, bool marked)> board, int number)
{
  var kvps = board.Where(x => !x.Value.marked);
  return kvps.Sum(x => x.Key) * number;
}