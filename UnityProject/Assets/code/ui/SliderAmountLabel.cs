using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.ui
{
  [RequireComponent(typeof(Text))]
  public class SliderAmountLabel : MonoBehaviour
  {
    [SerializeField] Slider slider = null;
    Text text = null;
    void Start()
    {
      UpdateText();
    }
    public void UpdateText()
    {
      if (text == null)
      {
        text = GetComponent<Text>();
      }

      text.text = slider.value.ToString();
    }
  }
}
