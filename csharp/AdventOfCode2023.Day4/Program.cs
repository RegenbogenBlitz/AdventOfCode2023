namespace AdventOfCode2023.Day4;

public class Program
{
    public static int Part1(string filename)
    {
        var total = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var mainSections = line.Split(':');
            //var cardNumber = int.Parse(mainSections[0].Split(' ')[1]);
            var scratchCardNumbers = mainSections[1].Split('|');

            var winningNumbersStrings =
                scratchCardNumbers[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var winningNumbers = winningNumbersStrings.Select(int.Parse).ToArray();
            
            var actualNumbersStrings = 
                scratchCardNumbers[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var actualNumbers = actualNumbersStrings.Select(int.Parse).ToArray();
            
            var matchingNumbers = winningNumbers.Intersect(actualNumbers).ToArray();
            var matchingNumberCount = matchingNumbers.Length;
            var score = matchingNumberCount == 0 ? 0 : Math.Pow(2, matchingNumberCount - 1);
            total += (int)score;
        }

        return total;
    }
}