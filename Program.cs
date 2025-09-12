using PointsBet_Backend_Online_Code_Test;

Console.WriteLine("Starting the StringFormatter app!");

var items = new string[] { "apple\"", null, "orange", "banana" };
var formattedString = StringFormatter.ToCommaSeparatedList(items, "\"");

Console.WriteLine(formattedString);