using UnityEngine;

namespace Assets.Scripts.GUI
{
  public class ButtonZapiskaOff : MonoBehaviour
  {
    [SerializeField] private AnimationClip backClip = null;
    protected virtual void OnPress(bool isPressed)
    {
      if (!isPressed)
      {
        GetComponent<Animation>().clip = backClip;
        GetComponent<Animation>().Play();
        Time.timeScale = 1;
      }
    }
  }
}
