namespace AdventOfCodeDay3;

public class Program
{
    public static int Part1(string filename)
    {
        var total = 0;
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

                    var anySymbol = false;
                    if(i>0)
                    {
                        var relevantLine = lineChars[i - 1];
                        var start = j>0 ? j-1 : j;
                        var relevantLength =
                            numberLength +
                            (numberLength + j > 0 ? 1 : 0) +
                            (j + numberLength < relevantLine.Length ? 1 : 0);
                        var relevantChars = relevantLine.Skip(start).Take(relevantLength).ToArray();
                        if(relevantChars.Any(c=>!char.IsDigit(c) && c != '.'))
                        {
                            anySymbol = true;
                        }
                    }

                    if (j > 0)
                    {
                        if(!char.IsDigit(lineOfChars[j-1]) && lineOfChars[j-1] != '.')
                        {
                            anySymbol = true;
                        }
                    }
                    
                    if (j + numberLength < lineOfChars.Length)
                    {
                        if(!char.IsDigit(lineOfChars[j+numberLength]) && lineOfChars[j+numberLength] != '.')
                        {
                            anySymbol = true;
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
                        var relevantChars = relevantLine.Skip(start).Take(relevantLength).ToArray();
                        if(relevantChars.Any(c=>!char.IsDigit(c) && c != '.'))
                        {
                            anySymbol = true;
                        }
                    }

                    if (anySymbol)
                    {
                        total += number;
                    }
                    j += numberLength - 1;
                }

            }
        }

        return total;
    }
}