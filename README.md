
# PointsBet StringFormatter (library)

Small C# library for formatting lists of strings.

## Requirements
- .NET 9.0 SDK+

## Structure

```
/src/PointsBet_Backend_Online_Code_Test          # class library
/tests/PointsBet_Backend_Online_Code_Test.Tests  # xUnit tests
StringFormatter.sln
```

## Quick Start (from repo root)
```bash
dotnet build
dotnet test
```

## API (v1)

**Namespace:** `PointsBet_Backend_Online_Code_Test`  
**Class:** `StringFormatter`

**Current**

```csharp
string ToCommaSeparatedList(string[] items, string quote)
```

## Example

```csharp
using PointsBet_Backend_Online_Code_Test;

var items = new[] { "apple", "orange", "banana" };
var csv = StringFormatter.ToCommaSeparatedList(items, "\"");
// "apple","orange", "banana"

```
Issues:
Typo, ToCommaSepatatedList
Does not handle null items, empty arrays and edge cases correctly.
Not optimised for performance
No input validation or error handling
Returns a string, not a List, which is a confusing naming choice.
Loop index begins at 1, which is confusing and error-prone.
Contains hardcoded values

Task:
Bugfix for existing ToCommaSeparatedList
Mark ToCommaSeparatedList as deprecated
Implement new method that replace/optimize the legacy ToCommaSeparatedList.
Add test suite to ensure All methods work as expected.
Add documentation to the new methods.
Update README that outlines issues, steps taken to complete task, overview of new methods, and how to run the app.
Update README with further considerations as to how to further improve the library.

---

## 🔑 Core Methods for current String Formatter

### 1. **Surround**

* **`Surround(value, wrapper)`** → adds the same wrapper before/after.
* **`Surround(value, prefix, suffix)`** → explicit sides.
* Example:

  * `Surround("apple", "*")` → `*apple*`
  * `Surround("apple", "[", "]")` → `[apple]`

---

### 2. **SurroundAndJoin**

* Applies `Surround` to each item in a collection, then concatenates.
* Overloads:

  * **`SurroundAndJoin(items, wrapper)`**
  * **`SurroundAndJoin(items, prefix, suffix)`**
* Example:

  * `SurroundAndJoin(["a","b"], "*")` → `*a**b*`
  * `SurroundAndJoin(["a","b"], "[", "]")` → `[a][b]`

---

### 3. **Join**

* **`Join(items, delimiter)`** → Concatenate with delimiter.
* Example:

  * `Join(["a","b","c"], ", ")` → `a, b, c`

*(this is basically `string.Join` in .NET or `"".join()` in Python, but worth including for completeness.)*

---

### 4. **ToList**

* **`ToList(value, delimiter)`** → Split a string into a list by delimiter.
* Example:

  * `ToList("a, b, c", ",")` → `["a", " b", " c"]`


✅ Goal is to keep things **tight and essential**, the **absolute core** for the current formatter is:

* `Surround`
* `SurroundAndJoin`
* `Join`
* `ToList`

This ensures our existing ToCommaSeparatedList is bug-free, optimised, replaced with the new methods.