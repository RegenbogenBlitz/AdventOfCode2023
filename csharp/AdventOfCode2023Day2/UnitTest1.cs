namespace AdventOfCode2023Day2;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var total = Program.Part1("example1.txt", 12, 13, 14);
        
        Assert.That(total, Is.EqualTo(8));
    }
    
    [Test]
    public void Part1Solution()
    {
        var total = Program.Part1("input.txt", 12, 13, 14);
        
        Assert.That(total, Is.EqualTo(2528));
    }
}