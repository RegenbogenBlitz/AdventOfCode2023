using NUnit.Framework.Constraints;

namespace AdventOfCode2023.Day6;

public class Program
{
    public static int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var timeLineJustNumbers = lines[0].Split(":")[1].Trim();
        var distanceLineJustNumbers = lines[1].Split(":")[1].Trim();

        var times = timeLineJustNumbers
            .Split(" ")
            .Where(s=>!string.IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();
        var distances = distanceLineJustNumbers
            .Split(" ")
            .Where(s=>!string.IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();
        
        var timeDistancePairs = times.Zip(distances).ToArray();
        
        var waysToBeatRecord = timeDistancePairs.Select(WaysToBeatRecord).ToArray();
        var product = waysToBeatRecord.Aggregate(1, (acc, x) => acc * x);
        return product;
    }

    private static int WaysToBeatRecord((int Time, int Distance) pair) => 
        Enumerable.Range(0, pair.Time + 1)
        .Count(i => i * (pair.Time - i) > pair.Distance);
}