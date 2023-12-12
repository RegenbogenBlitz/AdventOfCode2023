using System.Collections;

namespace AdventOfCode2023.Day4;

public class Program
{
    public static int Part1(string filename)
    {
        var cards = GetCards(filename);
        return cards.Sum(c =>
        {
            var matchingNumbers = c.WinningNumbers.Intersect(c.ActualNumbers).ToArray();
            var matchingNumberCount = matchingNumbers.Length;
            return matchingNumberCount == 0 ? 0 : (int)Math.Pow(2, matchingNumberCount - 1);
        });
    }
    
    public static int Part2(string filename)
    {
        var cards = GetCards(filename);
        var total = cards.Count;
        var cardReference = cards.ToDictionary(c=> c.Number, c=> c);
        var cardsToProcess = new Queue<Card>(cards.ToArray());
        while (cardsToProcess.Count > 0)
        {
            var card = cardsToProcess.Dequeue();
            var matchingNumbers = card.WinningNumbers.Intersect(card.ActualNumbers).ToArray();
            var matchingNumberCount = matchingNumbers.Length;
            
            for(var i = card.Number + 1; i <= card.Number+matchingNumberCount; i++)
            {
                if (cardReference.ContainsKey(i))
                {
                    cardsToProcess.Enqueue(cardReference[i]);
                    total++;
                }
            }
        }

        return total;
    }
    
    public static IReadOnlyCollection<Card> GetCards(string filename)
    {
        var cards = new List<Card>();
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var mainSections = line.Split(':');
            var cardNumber = int.Parse(mainSections[0].Split(' ').Last());
            var scratchCardNumbers = mainSections[1].Split('|');

            var winningNumbersStrings =
                scratchCardNumbers[0].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var winningNumbers = winningNumbersStrings.Select(int.Parse).ToArray();
            
            var actualNumbersStrings = 
                scratchCardNumbers[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            var actualNumbers = actualNumbersStrings.Select(int.Parse).ToArray();
            
            var card = new Card(cardNumber, winningNumbers, actualNumbers);
            cards.Add(card);
        }

        return cards;
    }
    
    public record Card(int Number, IReadOnlyCollection<int> WinningNumbers, IReadOnlyCollection<int> ActualNumbers);
}