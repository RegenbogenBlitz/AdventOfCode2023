namespace AdventOfCodeDay3;

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
}