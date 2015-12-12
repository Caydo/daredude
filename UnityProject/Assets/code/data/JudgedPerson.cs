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
    [JsonName("image")]
    public string Image;
    [JsonName("eyes")]
    public string Eyes;
    [JsonName("ears")]
    public string Ears;
    [JsonName("nose")]
    public string Nose;
    [JsonName("mouth")]
    public string Mouth;
    [JsonName("eyebrows")]
    public string Eyebrows;
    [JsonName("hair")]
    public string Hair;
  }
}
