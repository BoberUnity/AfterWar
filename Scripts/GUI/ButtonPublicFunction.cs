using UnityEngine;

public class ButtonPublicFunction : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private int id = 0;

  protected virtual void OnPress(bool isPressed)
  {
    if (isPressed)
    {
      if (id == 0)
        character.Jump();
      if (id == 1)
        character.Attack();
    }
    else
    {
      if (id == 1)
        character.EndAttack();
    }
  }
}
