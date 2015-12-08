using System;
/// <summary>
/// Base class for any class that'll be in a JSON data file. Actual data classes inherit from this
/// This way JSONUtil can return an array of any of these classes and it's still kosher.
/// E.g. Items[], Enemies[], Whatever[].
/// </summary>

[Serializable]
public class JSONFileDataPiece
{
}
