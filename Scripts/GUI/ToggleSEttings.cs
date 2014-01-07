using UnityEngine;

public class ToggleSEttings : MonoBehaviour
{
  public int a = 0;
  public bool v = false;
  public UIToggle toggle = null;

  private void Start()
  {
    v = toggle.value;
  }

  protected virtual void OnPress(bool isPressed)
  {
    
    if (!isPressed)
      a = 10;
  }

  protected virtual void Set()
  {
    v = toggle.value;
  }
}
