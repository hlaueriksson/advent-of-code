var numbers = File.ReadLines("input.txt").ToArray();
Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());

int PartOne()
{
  var size = 12;
  var gamma = new char[size];
  var epsilon = new char[size];
  for (int i = 0; i < size; i++)
  {
    var bits = numbers.Select(x => x[i]);
    gamma[i] = bits.Count(x => x == '1') > (numbers.Count() / 2) ? '1' : '0';
    epsilon[i] = bits.Count(x => x == '1') < (numbers.Count() / 2) ? '1' : '0';
  }
  return ToInt(new string(gamma)) * ToInt(new string(epsilon));
}

int PartTwo()
{
  var size = 12;
  var oxygen = new List<string>(numbers);
  var co2 = new List<string>(numbers);
  for (int i = 0; i < size; i++)
  {
    var bits = oxygen.Select(x => x[i]);
    var bit = bits.Count(x => x == '1') >= Math.Ceiling(oxygen.Count() / 2.0) ? '1' : '0';
    if (oxygen.Count() > 1) oxygen.RemoveAll(x => x[i] != bit);

    bits = co2.Select(x => x[i]);
    bit = bits.Count(x => x == '1') < Math.Ceiling(co2.Count() / 2.0) ? '1' : '0';
    if (co2.Count() > 1) co2.RemoveAll(x => x[i] != bit);
  }
  return ToInt(oxygen.Single()) * ToInt(co2.Single());
}

int ToInt(string binary)
{
  return Convert.ToInt32(binary, 2);
}