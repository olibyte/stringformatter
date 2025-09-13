namespace PointsBet_Backend_Online_Code_Test
{
    /// <summary>
    /// String formatting utilities for joining and splitting operations.
    /// Null elements in input arrays are treated as empty strings.
    /// All methods are pure (no side effects) and thread-safe.
    /// 
    /// Design Note: Avoids LINQ to reduce allocations and keep zero extra dependencies.
    /// </summary>
    public static class StringFormatter
    {
        private const string DefaultJoinDelimiter = ", ";
        private const string DefaultSplitDelimiter = ",";
        /// <summary>
        /// Creates a comma-separated string with quotes around each item.
        /// </summary>
        /// <param name="items">The items to format</param>
        /// <param name="quote">The quote string to wrap each item</param>
        /// <returns>A comma-separated string with quoted items (NOT a list/array)</returns>
        /// <exception cref="ArgumentNullException">Thrown when items is null</exception>
        /// <remarks>
        /// <para><strong>IMPORTANT:</strong> Despite the method name containing "List", this method returns a <see cref="string"/>, not a list or array.</para>
        /// 
        /// <para>This method is deprecated. Use the new Split() and Join() overloads instead for better performance and flexibility:</para>
        /// <list type="bullet">
        /// <item><description>For quoted comma-separated strings: <see cref="Join(string[], string, string, string)"/> with delimiter, quote as prefix, and quote as suffix</description></item>
        /// <item><description>For simple comma-separated strings: <see cref="Join(string[], string)"/> with delimiter only</description></item>
        /// <item><description>For splitting strings into arrays: <see cref="Split(string, string)"/></description></item>
        /// </list>
        /// 
        /// <para><strong>Example replacement:</strong></para>
        /// <code>
        /// // Old (deprecated):
        /// ToCommaSeparatedList(items, "\"")
        /// 
        /// // New (recommended):
        /// Join(items, ", ", "\"", "\"")
        /// </code>
        /// 
        /// <para><strong>Design Note:</strong> This method name and return type are preserved for backward compatibility. 
        /// The misleading "List" in the name is intentional to avoid breaking changes while encouraging migration to the new API.
        /// Will be removed in a future major version.</para>
        /// </remarks>
        [Obsolete("This method is deprecated. Use Join() overloads instead for better performance and flexibility. Planned removal in v2.", false)]
        public static string ToCommaSeparatedList(string[] items, string quote)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(quote);

            return Join(items, DefaultJoinDelimiter, quote, quote);
        }


        /// <summary>
        /// Concatenates items with default delimiter (", ").
        /// </summary>
        /// <param name="items">The items to join</param>
        /// <returns>The concatenated string with default delimiter</returns>
        public static string Join(string[] items)
        {
            return Join(items, DefaultJoinDelimiter);
        }

        /// <summary>
        /// Concatenates items with a delimiter.
        /// </summary>
        /// <param name="items">The items to join</param>
        /// <param name="delimiter">The delimiter to use between items</param>
        /// <returns>The concatenated string with delimiter</returns>
        public static string Join(string[] items, string delimiter)
        {
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(delimiter);

            return string.Join(delimiter, items);
        }

        /// <summary>
        /// Concatenates items with a single character delimiter.
        /// </summary>
        /// <param name="items">The items to join</param>
        /// <param name="delimiter">The single character delimiter to use between items</param>
        /// <returns>The concatenated string with delimiter</returns>
        /// <remarks>
        /// This method uses the optimised BCL string.Join(char, string[]) overload
        /// for better performance when using single character delimiters.
        /// </remarks>
        public static string Join(string[] items, char delimiter)
        {
            ArgumentNullException.ThrowIfNull(items);

            return string.Join(delimiter, items);
        }


        /// <summary>
        /// Concatenates items with a delimiter and adds prefix/suffix to each item.
        /// prefix/suffix may be empty or multi-character; when both are empty, this behaves like Join(items, delimiter) (fast path).
        /// Null elements in input arrays are treated as empty strings.
        /// </summary>
        /// <param name="items">The items to join</param>
        /// <param name="delimiter">The delimiter to use between items</param>
        /// <param name="prefix">The prefix to add before each item</param>
        /// <param name="suffix">The suffix to add after each item</param>
        /// <returns>The concatenated string with prefixed/suffixed items and delimiter</returns>
        public static string Join(string[] items, string delimiter, string prefix, string suffix)
        {
            // Validation order: primary parameter first, then secondary parameters
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(delimiter);
            ArgumentNullException.ThrowIfNull(prefix);
            ArgumentNullException.ThrowIfNull(suffix);

            if (prefix.Length == 0 && suffix.Length == 0)
                return string.Join(delimiter, items); // fast path

            var prefixedItems = new string[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                prefixedItems[i] = $"{prefix}{items[i]}{suffix}";
            }
            return string.Join(delimiter, prefixedItems);
        }

        /// <summary>
        /// Splits a string with default delimiter (",").
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <returns>Array of strings split by default delimiter</returns>
        /// <remarks>
        /// Note: Default delimiter is "," (no space), while Join() default is ", " (with space).
        /// This asymmetry is intentional to match common usage patterns.
        /// </remarks>
        public static string[] Split(string value)
        {
            return Split(value, DefaultSplitDelimiter);
        }

        /// <summary>
        /// Splits a string into an array by delimiter.
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="delimiter">The delimiter to split by</param>
        /// <returns>Array of strings split by the delimiter</returns>
        /// <remarks>
        /// Empty delimiters are not supported and will throw ArgumentException (ambiguous behavior).
        /// No trimming or empty-entry removal is performed.
        /// </remarks>
        public static string[] Split(string value, string delimiter)
        {
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(delimiter);
            // Empty delimiters cause ambiguous behavior (where to split)
            if (delimiter.Length == 0)
                throw new ArgumentException("Delimiter cannot be empty.", nameof(delimiter));

            return value.Split([delimiter], StringSplitOptions.None);
        }

        /// <summary>
        /// Splits a string by a single character delimiter.
        /// </summary>
        /// <param name="value">The string to split</param>
        /// <param name="delimiter">The single character delimiter to split by</param>
        /// <returns>Array of strings split by the delimiter</returns>
        /// <remarks>
        /// This method uses the optimised BCL string.Split(char, StringSplitOptions) overload
        /// for better performance when using single character delimiters.
        /// </remarks>

        public static string[] Split(string value, char delimiter)
        {
            ArgumentNullException.ThrowIfNull(value);

            return value.Split(delimiter, StringSplitOptions.None);
        }

    }
}
