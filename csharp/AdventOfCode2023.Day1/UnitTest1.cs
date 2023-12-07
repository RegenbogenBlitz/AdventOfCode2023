namespace AdventOfCode2023.Day1;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var total = Program.SumFileNumbers("example1.txt");
        
        Assert.That(total, Is.EqualTo(142));
    }
}