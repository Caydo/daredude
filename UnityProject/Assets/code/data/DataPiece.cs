using JsonFx.Json;
/// <summary>
/// The child class for a JSON data piece that holds the fields that are present in the data file.
/// These fields can various types (int, string, float, etc.), can be arrays of things, can be arrays
/// of other JSON file data piece classes, etc. It's all about how the JSON is structured.
/// </summary>
public class DataPiece : JSONFileDataPiece
{
  [JsonName("Number")]
  public int Number;
  [JsonName("Name")]
  public string Name;
  [JsonName("Power")]
  public int Power;
  [JsonName("Points")]
  public float Points;
  [JsonName("AdditionalDataPieces")]
  public AdditionalDataPiece[] AdditionalDataPieces;
}
