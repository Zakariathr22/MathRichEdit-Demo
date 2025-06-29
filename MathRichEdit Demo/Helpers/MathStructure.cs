namespace MathRichEdit_Demo.Helpers;

/// <summary>
/// Represents a structured mathematical expression (e.g., fraction, integral, matrix),
/// including image resources and markup representations.
/// </summary>
class MathStructure
{
    /// <summary>
    /// URI to the light theme image preview for the structure.
    /// </summary>
    public string LightImageSource { get; set; }

    /// <summary>
    /// URI to the dark theme image preview for the structure.
    /// </summary>
    public string DarkImageSource { get; set; }

    /// <summary>
    /// Descriptive name of the mathematical structure.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Unicode Notation Plain Text (NPT) representation.
    /// </summary>
    public string UnicodeNPT { get; set; }

    /// <summary>
    /// Rendered Unicode string or approximation of the structure.
    /// </summary>
    public string Unicode { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathStructure"/> class with explicit NPT and Unicode values.
    /// </summary>
    /// <param name="ImageUri">Relative path segment used to build image URIs.</param>
    /// <param name="name">Name of the structure.</param>
    /// <param name="unicodeNPT">NPT markup representation.</param>
    /// <param name="unicode">Rendered Unicode form.</param>
    public MathStructure(string ImageUri, string name, string unicodeNPT, string unicode)
    {
        LightImageSource = "ms-appx:///Assets/MathModeImages/" + ImageUri + "_Light.png";
        DarkImageSource = "ms-appx:///Assets/MathModeImages/" + ImageUri + "_Dark.png";
        Name = name;
        UnicodeNPT = unicodeNPT;
        Unicode = unicode;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MathStructure"/> class
    /// where the same string is used for both NPT and Unicode representations.
    /// </summary>
    /// <param name="ImageUri">Relative path segment used to build image URIs.</param>
    /// <param name="name">Name of the structure.</param>
    /// <param name="sharedUnicode">String used for both NPT and Unicode fields.</param>
    public MathStructure(string ImageUri, string name, string sharedUnicode)
    {
        LightImageSource = "ms-appx:///Assets/MathModeImages/" + ImageUri + "_Light.png";
        DarkImageSource = "ms-appx:///Assets/MathModeImages/" + ImageUri + "_Dark.png";
        Name = name;
        UnicodeNPT = sharedUnicode;
        Unicode = sharedUnicode;
    }
}
