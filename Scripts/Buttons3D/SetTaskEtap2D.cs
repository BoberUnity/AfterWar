using UnityEngine;

public class SetTaskEtap2D : MonoBehaviour
{
  [SerializeField] private Task task = null;
  [SerializeField] private int currentEtap = 0;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
      task.Etap = currentEtap;
  }
}
