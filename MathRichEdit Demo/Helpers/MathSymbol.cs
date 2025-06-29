namespace MathRichEdit_Demo.Helpers;

/// <summary>
/// Represents a mathematical symbol with its associated metadata.
/// </summary>
public class MathSymbol
{
    /// <summary>
    /// The rendered symbol character.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// A human-readable name describing the symbol.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Unicode Notation Plain Text (NPT) string, typically used in math markup.
    /// </summary>
    public string UnicodeNPT { get; set; }

    /// <summary>
    /// The standard Unicode code point (e.g., U+221E).
    /// </summary>
    public string Unicode { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathSymbol"/> class with specified values.
    /// </summary>
    /// <param name="value">The rendered symbol character.</param>
    /// <param name="name">The symbol's name.</param>
    /// <param name="unicodeNPT">The Unicode NPT representation.</param>
    /// <param name="unicode">The Unicode code point string.</param>
    public MathSymbol(string value, string name, string unicodeNPT, string unicode)
    {
        Value = value;
        Name = name;
        UnicodeNPT = unicodeNPT;
        Unicode = unicode;
    }
}
