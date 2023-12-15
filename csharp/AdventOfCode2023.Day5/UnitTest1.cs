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
    public void Part2Example()
    {
        var location = Program.Part2("example.txt");
        
        Assert.That(location, Is.EqualTo(46));
    }
    
    [Test]
    public void Part2Solution()
    {
        var location = Program.Part2("input.txt");
        
        Assert.That(location, Is.EqualTo(59370572));
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
    
    [Test]
    public void CombineMaps_Trival()
    {
        var seeds = new[] { 0, 5, 9 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 10)
            }),
            new Map("map2", new[]
            {
                new MapLine(0, 0, 10)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_FirstMapHasNoStart()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5)
            }),
            new Map("map2", new[]
            {
                new MapLine(0, 0, 20)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_SecondMapHasNoStart()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 20)
            }),
            new Map("map2", new[]
            {
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_FirstMapHasNoEnd()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 10)
            }),
            new Map("map2", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_SecondMapHasNoEnd()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5)
            }),
            new Map("map2", new[]
            {
                
                new MapLine(0, 0, 10)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_FirstMapHasNoMiddle()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(20, 20, 10)
            }),
            new Map("map2", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5),
                new MapLine(20, 20, 10)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_SecondMapHasNoMiddle()
    {
        var seeds = new[] { 10, 14, 15, 19 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(15, 10, 5),
                new MapLine(10, 15, 5),
                new MapLine(20, 20, 10)

            }),
            new Map("map2", new[]
            {
                new MapLine(0, 0, 10),
                new MapLine(20, 20, 10)
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_OverlappingRanges()
    {
        var seeds = new[] { 0, 9, 10, 14, 15, 19, 20, 29, 30, 39, 40, 44, 45, 49, 50, 59 };
        var mapSequence = new MapSequence(new[]
        {
            new Map("map1", new[]
            {
                new MapLine(15, 0, 15),
                new MapLine(0, 15, 15),
                
                new MapLine(40, 30, 10),
                new MapLine(50, 40, 10),
                new MapLine(30, 50, 10),
            }),
            new Map("map2", new[]
            {
                new MapLine(10, 0, 10),
                new MapLine(20, 10, 10),
                new MapLine(0, 20, 10),
                
                new MapLine(45, 30, 15),
                new MapLine(30, 45, 15),
            })
        });
        
        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_Example()
    {
        var (seeds, mapSequence) = Program.ReadFile("example.txt");

        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
    
    [Test]
    public void CombineMaps_Input()
    {
        var (seeds, mapSequence) = Program.ReadFile("input.txt");

        var combinedMap = mapSequence.CombineMaps();

        foreach (var seed in seeds)
        {
            var uncombined = mapSequence.Covert(seed);
            var combined = combinedMap.Covert(seed);
            Assert.That(uncombined, Is.EqualTo(combined));
        }
    }
}