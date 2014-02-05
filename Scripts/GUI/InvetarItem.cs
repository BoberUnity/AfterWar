using UnityEngine;

public class InvetarItem : MonoBehaviour 
{
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
      gameObject.SetActive(false);
  }
}
