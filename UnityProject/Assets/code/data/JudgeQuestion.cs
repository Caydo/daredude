using JsonFx.Json;

namespace Assets.code.data
{
  public class JudgeQuestion : JSONFileDataPiece
  {
    [JsonName("id")]
    public int ID;
    [JsonName("question")]
    public string Question;
    [JsonName("answer")]
    public string Answer;
    [JsonName("next")]
    public int Next;
    [JsonName("tagReveal")]
    public string TagReveal;
  }
}
