using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StairStarter : MonoBehaviour 
{
  [SerializeField] private Character character = null;
  [SerializeField] private float distX = 0.4f;

  protected virtual void OnPress(bool isPressed)
  {
    if (isPressed)
    {
      if (Mathf.Abs(transform.position.x - character.transform.position.x) < distX)
      {
        character.JumpToStair(transform.position.x - character.transform.position.x > 0);   //true - right; false - left
      }
    }
  }
}
