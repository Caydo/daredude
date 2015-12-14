using JsonFx.Json;

namespace Assets.code.data
{
  public class JudgedPerson : JSONFileDataPiece
  {
    [JsonName("id")]
    public int ID;
    [JsonName("name")]
    public string Name;
    [JsonName("bio")]
    public string Bio;
    [JsonName("startText")]
    public string StartText;
    [JsonName("tags")]
    public string[] Tags;
    [JsonName("questions")]
    public int[] Questions;
    [JsonName("hair")]
    public string Head;
    public bool Damned = false;
  }
}
