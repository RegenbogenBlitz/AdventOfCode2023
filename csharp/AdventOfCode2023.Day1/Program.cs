namespace AdventOfCode2023.Day1;

public static class Program
{
    public static int SumFileNumbers(string filename)
    {
        var total = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var chars = line.ToCharArray();
            var numbers = 
                chars
                    .Where(c => char.IsDigit(c))
                    .Select(c => int.Parse(c.ToString()))
                    .ToArray();
            var firstNumber = numbers.FirstOrDefault();
            var lastNumber = numbers.LastOrDefault();
            var sum = firstNumber * 10 + lastNumber;
            total += sum;
        }

        return total;
    }
}