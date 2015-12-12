using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.code.ui
{
  public class GoToSceneButton : MonoBehaviour
  {
    public void GoToScene(string sceneToGoTo)
    {
      SceneManager.LoadScene(sceneToGoTo);
    }
  }
}
