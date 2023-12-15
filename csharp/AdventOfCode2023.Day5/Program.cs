namespace AdventOfCode2023.Day5;

public class Program
{
    public static long Part1(string filename)
    {
        var (seeds, mapSequence) = ReadFile(filename);
        var combinedMap = mapSequence.CombineMaps();
        
        return seeds.Select(s=> combinedMap.Covert(s)).Min();
    }
    
    public static long Part2(string filename)
    {
        var (seeds, mapSequence) = ReadFile(filename);
        var combinedMap = mapSequence.CombineMaps();
        
        var seedRanges = new List<(long start, long end)>();
        for(int i = 0; i < seeds.Count; i++)
        {
            var seed = seeds.ElementAt(i);
            var nextSeed = seeds.ElementAt(i + 1);
            seedRanges.Add((seed, seed + nextSeed - 1));
            i++;
        }

        var orderedMapLines = combinedMap.MapLines.OrderBy(m => m.DestinationStart).ToArray();
        foreach (var mapLine in orderedMapLines)
        {
            var possibleSeeds = seedRanges
                .Where(r =>
                    r.start <= mapLine.SourceStart && mapLine.SourceStart <= r.end ||
                    r.start <= mapLine.SourceEnd && mapLine.SourceEnd <= r.end ||
                    mapLine.SourceStart <= r.start && r.start <= mapLine.SourceEnd ||
                    mapLine.SourceStart <= r.end && r.end <= mapLine.SourceEnd
                ).Select(r => Math.Max(r.start, mapLine.SourceStart))
                .ToArray();

            if (possibleSeeds.Any())
            {
                return mapLine.Covert(possibleSeeds.Min());
            } 
        }
        
        throw new Exception("No seed found");
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

public record MapLine(long DestinationStart, long SourceStart, long RangeLength)
{
    public long SourceEnd => SourceStart + RangeLength - 1;
    public long DestinationEnd => DestinationStart + RangeLength - 1;
    public long Covert(long source) => source - SourceStart + DestinationStart;
    public long Revert(long destination) => destination - DestinationStart + SourceStart;
};

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
            .Where(l => l.SourceStart <= source && source <= l.SourceEnd)
            .ToArray();

        if (matchingLines.Count() > 1)
        {
            throw new Exception("Multiple matching lines found");
        }
        
        var matchingLine = matchingLines.SingleOrDefault();
        
        return matchingLine?.Covert(source) ?? source;
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

    public Map CombineMaps()
    {
        if (Maps.Count == 0)
        {
            throw new Exception("No maps to combine");
        }
        
        var currentMapLines = Maps.First().MapLines;
        var nextMapLines = new List<MapLine>();

        for (var i = 1; i < Maps.Count; i++)
        {
            var inMapLines = currentMapLines;
            var outMapLines = Maps.ElementAt(i).MapLines;
            
            var maxInMapLineDestination = inMapLines.Max(m => m.DestinationEnd);
            var maxOutMapLineSource = outMapLines.Max(m => m.SourceEnd);
            var max = Math.Max(maxInMapLineDestination, maxOutMapLineSource);

            for (long j = 0; j <= max; j++)
            {
                var inMapLine = inMapLines
                    .SingleOrDefault(m => m.DestinationStart <= j && j <= m.DestinationEnd);

                if (inMapLine is null)
                {
                    var minInMapLineDestination = inMapLines.Min(m => m.DestinationStart);

                    if (j < minInMapLineDestination)
                    {
                        inMapLine = new MapLine(0, 0, minInMapLineDestination);
                    }
                    else if (j > maxInMapLineDestination)
                    {
                        inMapLine = new MapLine(
                            maxInMapLineDestination + 1, 
                            maxInMapLineDestination + 1,
                            max - maxInMapLineDestination);
                    }
                    else
                    {
                        var priorInMap = inMapLines
                            .Where(m => m.DestinationEnd < j)
                            .OrderByDescending(m => m.DestinationEnd)
                            .First();
                        var nextInMap = inMapLines
                            .Where(m => m.DestinationStart > j)
                            .OrderBy(m => m.DestinationStart)
                            .First();
                        
                        inMapLine = new MapLine(
                            priorInMap.DestinationEnd + 1,
                            priorInMap.DestinationEnd + 1,
                            nextInMap.DestinationStart - priorInMap.DestinationEnd - 1);
                    }
                }

                var outMapLine = outMapLines
                    .SingleOrDefault(m => m.SourceStart <= j && j <= m.SourceEnd);

                if (outMapLine is null)
                {
                    var minOutMapLineSource = outMapLines.Min(m => m.SourceStart);

                    if (j < minOutMapLineSource)
                    {
                        outMapLine = new MapLine(0, 0, minOutMapLineSource);
                    }
                    else if (j > maxOutMapLineSource)
                    {
                        outMapLine = new MapLine(
                            maxOutMapLineSource + 1, 
                            maxOutMapLineSource + 1,
                            max - maxOutMapLineSource);
                    }
                    else
                    {
                        var priorOutMap = outMapLines
                            .Where(m => m.SourceEnd < j)
                            .OrderByDescending(m => m.SourceEnd)
                            .First();
                        var nextOutMap = outMapLines
                            .Where(m => m.SourceStart > j)
                            .OrderBy(m => m.SourceStart)
                            .First();
                        
                        outMapLine = new MapLine(
                            priorOutMap.SourceEnd + 1,
                            priorOutMap.SourceEnd + 1,
                            nextOutMap.SourceStart - priorOutMap.SourceEnd - 1);
                    }
                }
                var newRange = Math.Min(inMapLine.DestinationEnd - j, outMapLine.SourceEnd - j) + 1;
                var newMapLine = new MapLine(outMapLine.Covert(j), inMapLine.Revert(j), newRange);
                nextMapLines.Add(newMapLine);

                j += newRange - 1;
            }
            
            currentMapLines = nextMapLines;
            nextMapLines = new List<MapLine>();
        }
        
        return new Map("combined", currentMapLines);
    }
}