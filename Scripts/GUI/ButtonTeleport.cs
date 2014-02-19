using UnityEngine;

public class ButtonTeleport: MonoBehaviour
{
  [SerializeField] private Transform hero = null;
  [SerializeField] private Vector3 pos;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      hero.position = pos;
    }
  }
}
