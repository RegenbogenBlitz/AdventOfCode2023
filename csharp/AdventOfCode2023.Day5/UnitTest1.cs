namespace AdventOfCode2023.Day5;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var location = Program.Part1("example.txt");
        
        Assert.That(location, Is.EqualTo(35));
    }
    
    [Test]
    public void Part1Solution()
    {
        var location = Program.Part1("input.txt");
        
        Assert.That(location, Is.EqualTo(806029445));
    }
    
    [Test]
    public void ReadFile()
    {
        var (seeds, mapSequence) = Program.ReadFile("example.txt");

        Assert.That(seeds, Is.EqualTo(new[] { 79, 14, 55, 13 }));
        Assert.That(mapSequence.Maps.Count, Is.EqualTo(7));
        
        var seedToSoil = mapSequence.Maps.ElementAt(0);
        Assert.That(seedToSoil.Name, Is.EqualTo("seed-to-soil"));
        Assert.That(seedToSoil.MapLines.Count, Is.EqualTo(2));
        
        var seedToSoil0 = seedToSoil.MapLines.ElementAt(0); 
        Assert.That(seedToSoil0.DestinationStart, Is.EqualTo(50));
        Assert.That(seedToSoil0.SourceStart, Is.EqualTo(98));
        Assert.That(seedToSoil0.RangeLength, Is.EqualTo(2));
        
        var humidityToLocation = mapSequence.Maps.ElementAt(6);
        Assert.That(humidityToLocation.Name, Is.EqualTo("humidity-to-location"));
        Assert.That(humidityToLocation.MapLines.Count, Is.EqualTo(2));
        
        var humidityToLocation1 = humidityToLocation.MapLines.ElementAt(1); 
        Assert.That(humidityToLocation1.DestinationStart, Is.EqualTo(56));
        Assert.That(humidityToLocation1.SourceStart, Is.EqualTo(93));
        Assert.That(humidityToLocation1.RangeLength, Is.EqualTo(4));
    }
}