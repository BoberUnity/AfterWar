using UnityEngine;

public class ButtonExit : MonoBehaviour
{
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
      Application.Quit();
  }
}
