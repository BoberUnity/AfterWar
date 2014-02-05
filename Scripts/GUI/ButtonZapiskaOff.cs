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
        animation.clip = backClip;
        animation.Play();
        Time.timeScale = 1;
      }
    }
  }
}
