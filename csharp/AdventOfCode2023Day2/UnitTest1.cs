namespace AdventOfCode2023Day2;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var total = Program.Part1("example.txt", 12, 13, 14);
        
        Assert.That(total, Is.EqualTo(8));
    }
    
    [Test]
    public void Part1Solution()
    {
        var total = Program.Part1("input.txt", 12, 13, 14);
        
        Assert.That(total, Is.EqualTo(2528));
    }
    
    [Test]
    public void Part2Example()
    {
        var total = Program.Part2("example.txt");
        
        Assert.That(total, Is.EqualTo(2286));
    }
    
    [Test]
    public void Part2Solution()
    {
        var total = Program.Part2("input.txt");
        
        Assert.That(total, Is.EqualTo(67363));
    }
}