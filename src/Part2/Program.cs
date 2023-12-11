var input = File.ReadAllLines("./input.txt");
var expandedInput = new List<string>(input);
var expansionSize = 1000000;
var addRows = new List<int>();
for (var j = 0; j < input.Length; j++)
{
    if (!input[j].Contains("#"))
    {
        addRows.Add(j);
    }
}

foreach (var addRow in addRows.OrderDescending())
{
    expandedInput[addRow] = string.Concat(Enumerable.Repeat('X', input[0].Length));
}

var addColumns = new List<int>();

for (var j = 0; j < input[0].Length; j++)
{
    var addColumn = true;
    for (var i = 0; i < input.Length; i++)
    {
        if (input[i][j] == '#')
        {
            addColumn = false;
            break;
        }
    }

    if (addColumn)
    {
        addColumns.Add(j);
    }
}

foreach (var addColumn in addColumns.OrderDescending())
{
    for (var i = 0; i < expandedInput.Count; i++)
    {
        expandedInput[i]= expandedInput[i].Remove(addColumn, 1).Insert(addColumn, "X"); ;
    }
}

var galaxyCounter = 1;
var map = new Dictionary<(int x, int y), int>();
for (var j = 0; j < expandedInput.Count; j++)
{
    var line = expandedInput[j];
    for (var i = 0; i < line.Length; i++)
    {
        if (line[i] == '#')
        {
            map.Add((i, j), galaxyCounter++);
        }
    }
}

var galaxyPathLengths = new List<long>();
var galaxyCount = map.Count(x => x.Value != 0);

for (var i = 1; i <= galaxyCount; i++)
{
    for (int j = i + 1; j <= galaxyCount; j++)
    {
        var galaxyA = map.First(x => x.Value == i);
        var galaxyB = map.First(x => x.Value == j);
        var horizontalString = expandedInput[galaxyA.Key.y].Substring(Math.Min(galaxyA.Key.x, galaxyB.Key.x), Math.Max(galaxyA.Key.x, galaxyB.Key.x)- Math.Min(galaxyA.Key.x, galaxyB.Key.x));
        var horizontalValue = horizontalString.Count(x => x != 'X') + (expansionSize * horizontalString.Count(x => x == 'X'));
        var verticalString = string.Concat(expandedInput.Select(x => x[galaxyA.Key.x]).ToArray()).Substring(Math.Min(galaxyA.Key.y, galaxyB.Key.y), Math.Max(galaxyA.Key.y, galaxyB.Key.y) - Math.Min(galaxyA.Key.y, galaxyB.Key.y));
        var verticalValue = verticalString.Count(x => x != 'X') + (expansionSize * verticalString.Count(x => x == 'X'));
        var shortestDistance = horizontalValue + verticalValue;
        galaxyPathLengths.Add(shortestDistance);
    }
}

Console.WriteLine(galaxyPathLengths.Sum());