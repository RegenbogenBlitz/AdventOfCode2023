namespace AdventOfCode2023.Day1;

public class Tests
{
    [Test]
    public void Part1Example()
    {
        var total = Program.Part1("example1.txt");
        
        Assert.That(total, Is.EqualTo(142));
    }
    
    [Test]
    public void Part1Solution()
    {
        var total = Program.Part1("input.txt");
        
        Assert.That(total, Is.EqualTo(54081));
    }
    
    [Test]
    public void Part2Example()
    {
        var total = Program.Part2("example2.txt");
        
        Assert.That(total, Is.EqualTo(281));
    }
    
    [Test]
    public void Part2Solution()
    {
        var total = Program.Part2("input.txt");
        
        Assert.That(total, Is.EqualTo(54649));
    }
}