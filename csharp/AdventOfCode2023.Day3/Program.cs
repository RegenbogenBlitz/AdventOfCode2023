namespace AdventOfCode2023.Day3;

public class Program
{
    public static IEnumerable<(int number, int numX, int numY, char symbol, int symX, int symY)> GetInfo(string filename)
    {
        var info = new List<(int number, int numX, int numY, char symbol, int symX, int symY)>();
        
        var lines = File.ReadAllLines(filename);
        var lineChars = lines.Select(line => line.ToCharArray()).ToArray();

        for(var i = 0; i < lineChars.Length; i++)
        { 
            var lineOfChars = lineChars[i];
            for(var j = 0; j < lineOfChars.Length; j++)
            {
                var c = lineOfChars[j];
                if (char.IsDigit(c))
                {
                    var numberLength = 0;
                    for (var k=j; k < lineOfChars.Length; k++)
                    {
                        if(char.IsDigit(lineOfChars[k]))
                        {
                            numberLength++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    var number = int.Parse(new string(lineOfChars, j, numberLength));

                    if(i>0)
                    {
                        var relevantLine = lineChars[i - 1];
                        var start = j>0 ? j-1 : j;
                        var relevantLength =
                            numberLength +
                            (numberLength + j > 0 ? 1 : 0) +
                            (j + numberLength < relevantLine.Length ? 1 : 0);
                        for(var k = start; k < start + relevantLength; k++)
                        {
                            if(!char.IsDigit(relevantLine[k]) && relevantLine[k] != '.')
                            {
                                info.Add((number, j, i, relevantLine[k], k, i - 1));
                            }
                        }
                    }

                    if (j > 0)
                    {
                        if(!char.IsDigit(lineOfChars[j-1]) && lineOfChars[j-1] != '.')
                        {
                            info.Add((number, j, i, lineOfChars[j-1], j-1, i));
                        }
                    }
                    
                    if (j + numberLength < lineOfChars.Length)
                    {
                        if(!char.IsDigit(lineOfChars[j+numberLength]) && lineOfChars[j+numberLength] != '.')
                        {
                            info.Add((number, j, i, lineOfChars[j+numberLength], j+numberLength, i));
                        }
                    }
                   
                    if(i<lineChars.Length-1)
                    {
                        var relevantLine = lineChars[i + 1];
                        var start = j>0 ? j-1 : j;
                        var relevantLength =
                            numberLength +
                            (numberLength + j > 0 ? 1 : 0) +
                            (j + numberLength < relevantLine.Length ? 1 : 0);
                        for(var k = start; k < start + relevantLength; k++)
                        {
                            if(!char.IsDigit(relevantLine[k]) && relevantLine[k] != '.')
                            {
                                info.Add((number, j, i, relevantLine[k], k, i + 1));
                            }
                        }
                    }

                    j += numberLength - 1;
                }

            }
        }

        return info;
    }

    public static int Part1(string fileName)
    {
        var info = GetInfo(fileName);
        var groups = info.GroupBy(n=> (n.number, n.numX, n.numY));
        var total = groups.Sum(g=>g.Key.number);
        return total;
    }
    
    public static int Part2(string fileName)
    {
        var info = GetInfo(fileName);
        var groups = info.GroupBy(n=> (n.symbol, n.symX, n.symY));
        var total = groups
            .Where(g => g.Key.symbol == '*' && g.Count() == 2)
            .Sum(g =>
            {
                var values = g.Select(n => n.number).ToArray();
                var product = values.Aggregate((a, b) => a * b);
                return product;
            });
        
        return total;
    }
}