namespace AdventOfCode2023.Day4;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var total = Program.Part1("example.txt");
        
        Assert.That(total, Is.EqualTo(13));
    }
    
    [Test]
    public void Part1Solution()
    {
        var total = Program.Part1("input.txt");
        
        Assert.That(total, Is.EqualTo(23941));
    }
    
    [Test]
    public void Part2Example()
    {
        var total = Program.Part2("example.txt");
        
        Assert.That(total, Is.EqualTo(30));
    }
    
    [Test]
    public void Part2Solution()
    {
        var total = Program.Part2("input.txt");
        
        Assert.That(total, Is.EqualTo(0));
    }
}