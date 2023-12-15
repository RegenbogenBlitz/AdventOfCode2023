namespace AdventOfCode2023.Day5;

public class Program
{
    public static long Part1(string filename)
    {
        var (seeds, mapSequence) = ReadFile(filename);
        
        return seeds.Select(s=> mapSequence.Covert(s)).Min();
    }
    
    public static (IReadOnlyCollection<long> seeds, MapSequence mapSequence) ReadFile(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var seeds = lines[0].Split(' ').Skip(1).Select(long.Parse).ToArray();
        var linesToProcess = lines.Skip(2).ToArray();
        
        var maps = new List<Map>();
        var mapLines = (List<MapLine>?)null;
        
        foreach (var line in linesToProcess)
        {
            if (line.EndsWith("map:"))
            {
                mapLines = new List<MapLine>();
                var mapIndex = line.IndexOf(" map", StringComparison.Ordinal);
                maps.Add(new Map(line.Substring(0, mapIndex), mapLines));
            }
            else if(line != string.Empty)
            {
                var mapLine = line.Split(' ').Select(long.Parse).ToArray();
                mapLines!.Add(new MapLine(mapLine[0], mapLine[1], mapLine[2]));
            }
        }

        var mapSequence = new MapSequence(maps);
        
        return (seeds, mapSequence);
    }
}

public record MapLine(long DestinationStart, long SourceStart, long RangeLength);

public class Map
{
    public Map(string name, IReadOnlyCollection<MapLine> mapLines)
    {
        Name = name;
        MapLines = mapLines;
    }
    
    public string Name { get; }
    
    public IReadOnlyCollection<MapLine> MapLines { get; }
    
    public long Covert(long source)
    {
        var matchingLines = MapLines
            .Where(l => l.SourceStart <= source && source <= l.SourceStart + l.RangeLength - 1)
            .ToArray();

        if (matchingLines.Count() > 1)
        {
            throw new Exception("Multiple matching lines found");
        }
        
        var matchingLine = matchingLines.SingleOrDefault();
        
        return matchingLine is not null
            ? source - matchingLine.SourceStart + matchingLine.DestinationStart
            : source;
    }
}

public class MapSequence
{
    public MapSequence(IReadOnlyCollection<Map> maps)
    {
        Maps = maps;
    }
    
    public IReadOnlyCollection<Map> Maps { get; }
    
    public long Covert(long source)
    {
        var result = source;
        foreach (var map in Maps)
        {
            result = map.Covert(result);
        }

        return result;
    }
}