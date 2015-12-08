using JsonFx.Json;
/// <summary>
/// A secondary class to demonstrate that DataPiece can have an array of objects that are also themselves data objects.
/// </summary>
public class AdditionalDataPiece : JSONFileDataPiece
{
  [JsonName("Name")]
  public string Name;
}
