namespace AdventOfCode2023.Day2;

public class Program
{
    public static int Part1(string filename, int possibleRed, int possibleGreen, int possibleBlue)
    {
        var total = 0;

        var games = GetGames(filename);

        for(var i =0; i < games.Length; i++)
        {
            var game = games[i];
            if(game.All(r=> r.red <= possibleRed && r.green <= possibleGreen && r.blue <= possibleBlue))
            {
                total += i + 1;
            }
        }

        return total;
    }

    public static int Part2(string filename)
    {
        var total = 0;

        var games = GetGames(filename);

        for(var i =0; i < games.Length; i++)
        {
            var game = games[i];
            var maxRed = game.Max(r => r.red);
            var maxGreen = game.Max(r => r.green);
            var maxBlue = game.Max(r => r.blue);
            var product = maxRed * maxGreen * maxBlue;
            total += product;
        }

        return total;
    }
    
    public static IReadOnlyCollection<(int red, int green, int blue)>[] GetGames(string filename)
    {
        var games = new List<IReadOnlyCollection<(int red, int green, int blue)>>();
        
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var gameNumber = int.Parse(line.Split(':')[0].Split(' ')[1]);
            var gameTexts = line.Split(':')[1].Split(';');
            var rounds = new List<(int red, int green, int blue)>();
            foreach (var game in gameTexts)
            {
                var red = 0;
                var green = 0;
                var blue = 0;
                var colors = game.Split(',');
                foreach (var color in colors)
                {
                    var trimmedColor = color.Trim();
                    if (trimmedColor.Contains("red"))
                    {
                        red = int.Parse(trimmedColor.Split(' ')[0]);
                    }
                    else if (trimmedColor.Contains("green"))
                    {
                        green = int.Parse(trimmedColor.Split(' ')[0]);
                    }
                    else if (trimmedColor.Contains("blue"))
                    {
                        blue = int.Parse(trimmedColor.Split(' ')[0]);
                    }
                }

                rounds.Add((red, green, blue));
            }

            games.Add(rounds);
        }

        return games.ToArray();
    }
}