# PointsBet StringFormatter Library

A small, zero-dependency C# library for string formatting operations, providing light-weight efficient methods for joining, splitting, and formatting string collections.

## Summary of Original Function

The original `StringFormatter` class contained a single method `ToCommaSeparatedList` that was designed to create comma-separated strings with quotes around each item. However, this implementation had several limitations that made it unreliable and confusing to use.

**Original Method:**

```csharp
string ToCommaSeparatedList(string[] items, string quote)
```

**Original Limitations:**

* Method name contained a typo (`ToCommaSepatatedList`)
* Misleading name suggesting it returns a List when it actually returns a string
* Poor handling of null items and edge cases
* No input validation or error handling
* Performance issues with inefficient string operations
* Hardcoded values and confusing loop indexing
* Limited functionality for a string formatting library

## Issues Identified

### Bugs and Limitations

1. **Method Name Confusion**: Despite being called "ToCommaSeparatedList", the method returns a `string`, not a `List<string>`
2. **Null Handling**: Poor handling of null items in arrays
3. **Edge Cases**: Inconsistent behaviour with empty arrays and special characters
4. **Input Validation**: No validation for null parameters, leading to potential runtime exceptions
5. **Performance**: Inefficient string concatenation without proper optimisation

### Design Issues

1. **Limited Scope**: Single-purpose method in a class that should handle multiple string operations
2. **Hardcoded Values**: Inflexible delimiter and formatting options
3. **Poor API Design**: Confusing method signature and return type
4. **No Extensibility**: No clear path for adding new formatting methods

## Strategy

### 1. Extend the Library

* Implement a set of core string formatting methods
* Add multiple overloads for different use cases (char vs string delimiters)
* Provide both simple and advanced formatting options

### 2. Resolve Critical Issues

* Fix the original `ToCommaSeparatedList` method to handle edge cases properly
* Add proper input validation with meaningful error messages
* Ensure consistent behaviour across all scenarios

### 3. Deprecation Policy

* The original method is marked `[Obsolete]` with migration guidance.
* It remains available for backward compatibility, but **all new code should migrate to `Join` or `Split` overloads**.
* This method will be removed in a future major version.

### 4. Implement New Optimised Methods

* **Join Methods**: Multiple overloads for different delimiter types and formatting options
* **Split Methods**: Efficient string splitting with proper validation
* **Performance Optimisations**: Use BCL optimisations for char delimiters, fast paths for common cases

## Getting Started

### Requirements

* .NET 9.0 SDK or later

### Installation

This is a class library that can be referenced in your .NET projects. Simply add a project reference to the compiled library.

### Basic Usage

```csharp
using PointsBet_Backend_Online_Code_Test;

// Simple joining
var items = new[] { "apple", "orange", "banana" };
var result = StringFormatter.Join(items, ", ");
// Result: "apple, orange, banana"

// Joining with quotes (replaces ToCommaSeparatedList)
var quoted = StringFormatter.Join(items, ", ", "\"", "\"");
// Result: "\"apple\", \"orange\", \"banana\""

// Splitting strings
var parts = StringFormatter.Split("a,b,c", ",");
// Result: ["a", "b", "c"]
```

### Migration from Legacy Method

```csharp
// Old (deprecated) - still works but shows warning
var oldResult = StringFormatter.ToCommaSeparatedList(items, "\"");

// New (recommended) - better performance and flexibility
var newResult = StringFormatter.Join(items, ", ", "\"", "\"");
```

⚠️ **Note**: The legacy method is preserved only for backward compatibility. It should not be used in new code.

## Build

### From Repository Root

```bash
# Build the solution
dotnet build

# Build in Release mode
dotnet build --configuration Release

# Build specific project
dotnet build src/PointsBet_Backend_Online_Code_Test/PointsBet_Backend_Online_Code_Test.csproj
```

### Project Structure

```
/src/PointsBet_Backend_Online_Code_Test/          # Main class library
/tests/PointsBet_Backend_Online_Code_Test.Tests/  # xUnit test suite
StringFormatter.sln                               # Solution file
```

## Test

### Run All Tests

```bash
# Run tests from repository root
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test project
dotnet test tests/PointsBet_Backend_Online_Code_Test.Tests/
```

### Test Coverage

The test suite includes coverage for:

* **Legacy Method**: `ToCommaSeparatedList` with edge cases
* **New Join Methods**: All overloads with various parameter combinations
* **Split Methods**: String and character delimiter variations
* **Error Handling**: Null parameter validation and exception testing
* **Edge Cases**: Empty arrays, null items, special characters

### Test Categories

* **Unit Tests**: Individual method behaviour
* **Integration Tests**: Method combinations and workflows
* **Edge Case Tests**: Boundary conditions and error scenarios
* **Performance Tests**: Validation of optimisation claims

## API Reference

### Core Methods

#### Join Methods

```csharp
// Simple joining with default delimiter
string Join(string[] items)

// Custom delimiter
string Join(string[] items, string delimiter)
string Join(string[] items, char delimiter)

// With prefix/suffix for each item
string Join(string[] items, string delimiter, string prefix, string suffix)
```

#### Split Methods

```csharp
// Split with default delimiter
string[] Split(string value)

// Custom delimiter
string[] Split(string value, string delimiter)
string[] Split(string value, char delimiter)
```

#### Legacy Method (Deprecated)

```csharp
[Obsolete("Use Join() overloads instead")]
string ToCommaSeparatedList(string[] items, string quote)
```

## Alternatives to This Library

While this library provides lightweight, zero-dependency utilities, many real-world scenarios can be solved with existing .NET features or established libraries:

* **.NET Built-Ins**: `string.Join`, `string.Split`, and interpolated strings cover most basic cases.
* **System.Text.Formatting**: Offers efficient formatting for performance-critical paths.
* **CSV Libraries**: For CSV-style quoting/escaping, consider packages like [CsvHelper](https://joshclose.github.io/CsvHelper/).

Built-in methods are best suited when you want **predictable, dependency-free helpers**, but specialised libraries may be more appropriate for complex use cases.

## Next Steps

### Additional Features

1. **IEnumerable Support**: Extend methods to work with `IEnumerable<string>` and `IEnumerable<T>`
2. **Async Methods**: Add async versions for I/O-bound string operations
3. **Custom Delimiters**: Support for multi-character and regex delimiters
4. **More Core Methods**: Built-in string formatting utils such as `ToJson`, `ToXml`, and alternatively `CsvReader` and `CsvWriter` classes/services.
5. **Encoding Support**: Handle different string encodings properly