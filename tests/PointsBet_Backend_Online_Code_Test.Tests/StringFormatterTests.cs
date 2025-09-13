using Xunit;
using PointsBet_Backend_Online_Code_Test;

namespace PointsBet_Backend_Online_Code_Test.Tests;

public class StringFormatterTests
{
    [Fact]
    public void ToCommaSeparatedList_WithValidItems_ReturnsCorrectlyFormattedString()
    {
        // Arrange
        var items = new[] { "apple", "orange", "banana" };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("\"apple\", \"orange\", \"banana\"", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithSingleItem_ReturnsCorrectlyFormattedString()
    {
        // Arrange
        var items = new[] { "apple" };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("\"apple\"", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithEmptyArray_ReturnsEmptyString()
    {
        // Arrange
        var items = new string[0];
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithNullArray_ThrowsArgumentNullException()
    {
        // Arrange
        string[]? items = null;
        var quote = "\"";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => StringFormatter.ToCommaSeparatedList(items!, quote));
    }

    [Fact]
    public void ToCommaSeparatedList_WithDifferentQuote_ReturnsCorrectlyFormattedString()
    {
        // Arrange
        var items = new[] { "apple", "orange", "banana" };
        var quote = "'";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("'apple', 'orange', 'banana'", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithEmptyQuote_ReturnsCorrectlyFormattedString()
    {
        // Arrange
        var items = new[] { "apple", "orange", "banana" };
        var quote = "";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("apple, orange, banana", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithNullItems_HandlesCorrectly()
    {
        // Arrange
        var items = new string?[] { "apple", null, "banana" };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items!, quote);

        // Assert
        Assert.Equal("\"apple\", \"\", \"banana\"", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithEmptyStringItems_HandlesCorrectly()
    {
        // Arrange
        var items = new[] { "apple", "", "banana" };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("\"apple\", \"\", \"banana\"", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithSpecialCharacters_HandlesCorrectly()
    {
        // Arrange
        var items = new[] { "item,with,comma", "item\"with\"quote", "item'with'apostrophe" };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("\"item,with,comma\", \"item\"with\"quote\", \"item'with'apostrophe\"", result);
    }

    [Fact]
    public void ToCommaSeparatedList_WithWhitespaceItems_HandlesCorrectly()
    {
        // Arrange
        var items = new[] { " apple ", " orange ", " banana " };
        var quote = "\"";

        // Act
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        // Assert
        Assert.Equal("\" apple \", \" orange \", \" banana \"", result);
    }
}
