var input = File.ReadAllLines("./test_input.txt");
var expandedInput = new List<string>(input);

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
    expandedInput.Insert(addRow, input[addRow]);
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
        expandedInput[i] = expandedInput[i].Insert(addColumn, ".");
    }
}

var galaxyCounter = 1;
var map = new Dictionary<(int x, int y), int>();
for (var j = 0; j < expandedInput.Count; j++)
{
    var line = expandedInput[j];
    for (var i = 0; i < line.Length; i++)
    {
        map.Add((i, j), line[i] == '#' ? galaxyCounter++ : 0 );
    }
}

var galaxyPathLengths = new List<long>();
var galaxyCount = map.Count(x => x.Value != 0);
for (var i = 1; i <= galaxyCount; i++)
{
    for (int j = i+1; j <= galaxyCount; j++)
    {
        var galaxyA = map.First(x => x.Value == i);
        var galaxyB = map.First(x => x.Value == j);
        var shortestDistance = Math.Abs(galaxyA.Key.x - galaxyB.Key.x) + Math.Abs(galaxyA.Key.y - galaxyB.Key.y);
        galaxyPathLengths.Add(shortestDistance);

    }
}

Console.WriteLine(galaxyPathLengths.Sum());