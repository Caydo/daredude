using Assets.code.data;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class JudgedPersonDisplayImage : MonoBehaviour
{
  public enum BodyPart
  {
    Eyes = 0,
    Ears = 1,
    Nose = 2,
    Mouth = 3,
    Eyebrows = 4,
    Hair = 5
  }
  public BodyPart BodypartToDisplay = BodyPart.Eyes;
  const string EYES_ART_FILE_PREFIX = "art/headshots/";
  const string EARS_ART_FILE_PREFIX = "art/headshots/";
  const string NOSE_ART_FILE_PREFIX = "art/headshots/";
  const string EYEBROWS_ART_FILE_PREFIX = "art/headshots/";
  const string MOUTH_ART_FILE_PREFIX = "art/headshots/";
  const string HAIR_ART_FILE_PREFIX = "art/headshots/";

  public void DisplayNewPerson(JudgedPerson person)
  {
    string filePathForImage = string.Empty;

    switch(BodypartToDisplay)
    {
      case BodyPart.Eyes:
        filePathForImage = string.Format("{0}{1}", EYES_ART_FILE_PREFIX, person.Image);
        break;
      case BodyPart.Ears:
        filePathForImage = string.Format("{0}{1}", EARS_ART_FILE_PREFIX, person.Image);
        break;
      case BodyPart.Nose:
        filePathForImage = string.Format("{0}{1}", NOSE_ART_FILE_PREFIX, person.Image);
        break;
      case BodyPart.Mouth:
        filePathForImage = string.Format("{0}{1}", EYEBROWS_ART_FILE_PREFIX, person.Image);
        break;
      case BodyPart.Eyebrows:
        filePathForImage = string.Format("{0}{1}", MOUTH_ART_FILE_PREFIX, person.Image);
        break;
      case BodyPart.Hair:
        filePathForImage = string.Format("{0}{1}", HAIR_ART_FILE_PREFIX, person.Image);
        break;
    }

    GetComponent<Image>().sprite = Resources.Load<Sprite>(filePathForImage);
  }
}
