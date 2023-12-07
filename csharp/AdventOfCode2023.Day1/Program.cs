namespace AdventOfCode2023.Day1;

public static class Program
{
    public static int Part1(string filename)
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
    
    public static int Part2(string filename)
    {
        var numberWords = new[]
        {
            ("one", 1),
            ("two", 2),
            ("three", 3),
            ("four", 4),
            ("five", 5),
            ("six", 6),
            ("seven", 7),
            ("eight", 8),
            ("nine", 9)
        };
        
        var total = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var chars = line.ToCharArray();

            var firstNumber = (int?)null;
            var lastNumber = (int?)null;
            for (var i = 0; i < chars.Length; i++)
            {
                var c = chars[i];
                if (char.IsDigit(c))
                {
                    firstNumber ??= int.Parse(c.ToString());
                    lastNumber = int.Parse(c.ToString());
                    continue;
                }

                foreach (var numberWord in numberWords)
                {
                    var word = numberWord.Item1;
                    var number = numberWord.Item2;
                    if (i + word.Length > chars.Length)
                    {
                        continue;
                    }
                    
                    var wordChars = new string(chars.Skip(i).Take(word.Length).ToArray());
                    if (word == wordChars)
                    {
                        firstNumber ??= number;
                        lastNumber = number;
                    }
                }
            }
            
            if(firstNumber is not null && lastNumber is not null)
            {
                var sum = firstNumber.Value * 10 + lastNumber.Value;
                total += sum;
            }
        }

        return total;
    }
}