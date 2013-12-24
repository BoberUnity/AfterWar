using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StairStarter : MonoBehaviour 
{
  [SerializeField] private Character character = null;
  [SerializeField] private float dist = 0.1f;

  protected virtual void OnPress(bool isPressed)
  {
    if (Vector3.Distance(transform.position, character.transform.position) < dist)
    {
      character.Jump();
    }
  }
}
