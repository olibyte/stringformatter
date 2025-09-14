namespace PointsBet_Backend_Online_Code_Test.Tests;

/// <summary>
/// Test suite for StringFormatter methods.
/// Note: Deprecation warnings for ToCommaSeparatedList are intentionally suppressed
/// as this is an intentional design choice to guide users toward the new API.
/// </summary>
public class StringFormatterTests
{
#pragma warning disable CS0618 // Type or member is obsolete - Intentionally testing deprecated method
    [Theory]
    [InlineData(new[] { "apple", "orange", "banana" }, "\"", "\"apple\", \"orange\", \"banana\"")]
    [InlineData(new[] { "apple" }, "\"", "\"apple\"")]
    [InlineData(new string[0], "\"", "")]
    [InlineData(new[] { "apple", "orange", "banana" }, "'", "'apple', 'orange', 'banana'")]
    [InlineData(new[] { "apple", "orange", "banana" }, "", "apple, orange, banana")]
    public void ToCommaSeparatedList_ItemsArrayAndQuote_ReturnsExpectedResult(string[] items, string quote, string expected)
    {
        var result = StringFormatter.ToCommaSeparatedList(items, quote);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToCommaSeparatedList_NullItems_ThrowsArgumentNullException()
    {
        string[]? items = null;
        var quote = "\"";

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.ToCommaSeparatedList(items!, quote));
        Assert.Equal("items", ex.ParamName);
    }

    [Fact]
    public void ToCommaSeparatedList_NullQuote_ThrowsArgumentNullException()
    {
        var items = new[] { "apple", "orange" };
        string? quote = null;

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.ToCommaSeparatedList(items, quote!));
        Assert.Equal("quote", ex.ParamName);
    }

    [Theory]
    [InlineData(new string?[] { "apple", null, "banana" }, "\"", "\"apple\", \"\", \"banana\"")]
    [InlineData(new[] { "apple", "", "banana" }, "\"", "\"apple\", \"\", \"banana\"")]
    [InlineData(new[] { "item,with,comma", "item\"with\"quote", "item'with'apostrophe" }, "\"", "\"item,with,comma\", \"item\"with\"quote\", \"item'with'apostrophe\"")]
    [InlineData(new[] { " apple ", " orange ", " banana " }, "\"", "\" apple \", \" orange \", \" banana \"")]
    public void ToCommaSeparatedList_EdgeCases_ReturnsExpectedResult(string?[] items, string quote, string expected)
    {
        var result = StringFormatter.ToCommaSeparatedList(items!, quote);

        Assert.Equal(expected, result);
    }
#pragma warning restore CS0618 // Type or member is obsolete


    // Join Tests

    [Theory]
    [InlineData(new[] { "a", "b", "c" }, ", ", "a, b, c")]
    [InlineData(new[] { "apple" }, ", ", "apple")]
    [InlineData(new string[0], ", ", "")]
    [InlineData(new[] { "a", "b", "c" }, "", "abc")]
    [InlineData(new[] { "apple", "orange", "banana" }, " | ", "apple | orange | banana")]
    public void Join_StringDelimiter_ReturnsExpectedResult(string[] items, string delimiter, string expected)
    {
        var result = StringFormatter.Join(items, delimiter);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Join_NullItems_ThrowsArgumentNullException()
    {
        string[]? items = null;
        var delimiter = ", ";

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Join(items!, delimiter));
        Assert.Equal("items", ex.ParamName);
    }

    [Fact]
    public void Join_NullDelimiter_ThrowsArgumentNullException()
    {
        var items = new[] { "a", "b" };
        string? delimiter = null;

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Join(items, delimiter!));
        Assert.Equal("delimiter", ex.ParamName);
    }

    [Theory]
    [InlineData(new[] { "apple", "orange", "banana" }, '|', "apple|orange|banana")]
    [InlineData(new[] { "apple" }, '|', "apple")]
    public void Join_CharDelimiter_ReturnsExpectedResult(string[] items, char delimiter, string expected)
    {
        var result = StringFormatter.Join(items, delimiter);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(new[] { "apple", "orange", "banana" }, ", ", "[", "]", "[apple], [orange], [banana]")]
    [InlineData(new[] { "apple" }, ", ", "<", ">", "<apple>")]
    [InlineData(new string[0], ", ", "[", "]", "")]
    public void Join_PrefixSuffix_ReturnsExpectedResult(string[] items, string delimiter, string prefix, string suffix, string expected)
    {
        var result = StringFormatter.Join(items, delimiter, prefix, suffix);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Join_CharStringDelimiter_ProducesIdenticalResults()
    {
        var items = new[] { "apple", "orange", "banana" };

        var charResult = StringFormatter.Join(items, '|');
        var stringResult = StringFormatter.Join(items, "|");

        Assert.Equal(charResult, stringResult);
        Assert.Equal("apple|orange|banana", charResult);
    }

    [Fact]
    public void Join_NullPrefix_ThrowsArgumentNullException()
    {
        var items = new[] { "apple", "orange" };
        var delimiter = ", ";
        string? prefix = null;
        var suffix = "]";

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Join(items, delimiter, prefix!, suffix));
        Assert.Equal("prefix", ex.ParamName);
    }

    [Fact]
    public void Join_NullSuffix_ThrowsArgumentNullException()
    {
        var items = new[] { "apple", "orange" };
        var delimiter = ", ";
        var prefix = "[";
        string? suffix = null;

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Join(items, delimiter, prefix, suffix!));
        Assert.Equal("suffix", ex.ParamName);
    }

    // Split Tests

    [Theory]
    [InlineData("a|b|c", '|', new[] { "a", "b", "c" })]
    [InlineData("apple", '|', new[] { "apple" })]
    public void Split_CharDelimiter_ReturnsExpectedResult(string value, char delimiter, string[] expected)
    {
        var result = StringFormatter.Split(value, delimiter);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("a, b, c", ",", new[] { "a", " b", " c" })]
    [InlineData("apple", ",", new[] { "apple" })]
    [InlineData("", ",", new[] { "" })]
    [InlineData("apple|orange|banana", "|", new[] { "apple", "orange", "banana" })]
    [InlineData("apple, orange, banana", ", ", new[] { "apple", "orange", "banana" })]
    public void Split_StringDelimiter_ReturnsExpectedResult(string value, string delimiter, string[] expected)
    {
        var result = StringFormatter.Split(value, delimiter);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Split_NullValue_ThrowsArgumentNullException()
    {
        string? value = null;
        var delimiter = ",";

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Split(value!, delimiter));
        Assert.Equal("value", ex.ParamName);
    }

    [Fact]
    public void Split_NullDelimiter_ThrowsArgumentNullException()
    {
        var value = "a,b,c";
        string? delimiter = null;

        var ex = Assert.Throws<ArgumentNullException>(() => StringFormatter.Split(value, delimiter!));
        Assert.Equal("delimiter", ex.ParamName);
    }

    [Fact]
    public void Split_EmptyDelimiter_ThrowsArgumentException()
    {
        var value = "apple";
        var delimiter = "";

        var ex = Assert.Throws<ArgumentException>(() => StringFormatter.Split(value, delimiter));
        Assert.Equal("delimiter", ex.ParamName);
    }

}
