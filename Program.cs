using PointsBet_Backend_Online_Code_Test;

Console.WriteLine("Starting the StringFormatter app!");

var items = new string[] { "apple", "orange", "banana" };
var formattedString = StringFormatter.ToCommaSepatatedList(items, "\"");

Console.WriteLine(formattedString);