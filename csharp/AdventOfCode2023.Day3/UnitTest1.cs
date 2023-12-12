namespace AdventOfCode2023.Day3;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var total = Program.Part1("example.txt");
        
        Assert.That(total, Is.EqualTo(4361));
    }
    
    [Test]
    public void Part1Solution()
    {
        var total = Program.Part1("input.txt");
        
        Assert.That(total, Is.EqualTo(544664));
    }
    
    [Test]
    public void Part2Example()
    {
        var total = Program.Part2("example.txt");
        
        Assert.That(total, Is.EqualTo(467835));
    }
    
    [Test]
    public void Part2Solution()
    {
        var total = Program.Part2("input.txt");
        
        Assert.That(total, Is.EqualTo(84495585));
    }
}