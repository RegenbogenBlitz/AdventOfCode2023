using NUnit.Framework.Constraints;

namespace AdventOfCode2023.Day6;

public class Program
{
    public static long Part1(string filename)
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
        
        var waysToBeatRecord = timeDistancePairs.Select(p=>WaysToBeatRecord(p.First,p.Second)).ToArray();
        var product = waysToBeatRecord.Aggregate(1, (long acc, long x) => acc * x);
        return product;
    }
    
    public static long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var timeLineJustNumbers = lines[0].Split(":")[1].Where(c=>!char.IsWhiteSpace(c));
        var distanceLineJustNumbers = lines[1].Split(":")[1].Where(c=>!char.IsWhiteSpace(c));

        var time = long.Parse(new string(timeLineJustNumbers.ToArray()));
        var distance = long.Parse(new string(distanceLineJustNumbers.ToArray()));
        
        return WaysToBeatRecord(time,distance);
    }

    private static long WaysToBeatRecord(long time, long distance)
    {
        // calculate smallest i such that  i * (time - i) > distance
        // i * time - i * i > distance
        
        // i * i - i * time + distance < 0
        // a = 1, b = -time, c = distance
        // i = (-b +- sqrt(b*b - 4*a*c)) / 2*a
        var a = 1;
        var b = -time;
        var c = distance;
        var sqrt = Math.Sqrt(b*b - 4*a*c);
        var i1 = (-b + sqrt) / (2*a);
        var i2 = (-b - sqrt) / (2*a);
        
        // i rounded up to nearest long
        var iTop = (long)i1;
        if(iTop*(time-iTop) == distance)
        {
            iTop--;
        }
        if(iTop*(time-iTop) <= distance)
        {
            throw new Exception("iTop is out of range");
        }
        if((iTop+1)*(time-(iTop+1)) > distance)
        {
            throw new Exception("iTop is not the top");
        }
        
        var iBottom = (long)Math.Ceiling(i2);
        if(iBottom*(time-iBottom) == distance)
        {
            iBottom++;
        }
        if(iBottom*(time-iBottom) <= distance)
        {
            throw new Exception("iBottom is out of range");
        }
        if((iBottom-1)*(time-(iBottom-1)) > distance)
        {
            throw new Exception("iBottom is not the bottom");
        }
        
        return iTop - iBottom + 1;
    } 
        

}