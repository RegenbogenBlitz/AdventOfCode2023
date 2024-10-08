namespace AdventOfCode2023.Day6;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var result = Program.Part1("example.txt");
        
        Assert.That(result, Is.EqualTo(288));
    }
    
    [Test]
    public void Part1Solution()
    {
        var result = Program.Part1("input.txt");
        
        Assert.That(result, Is.EqualTo(771628));
    }
    
    [Test]
    public void Part2Example()
    {
        var result = Program.Part2("example.txt");
        
        Assert.That(result, Is.EqualTo(71503));
    }
    
    [Test]
    public void Part2Solution()
    {
        var result = Program.Part2("input.txt");
        
        Assert.That(result, Is.EqualTo(27363861));
    }
}