using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StairStarter : MonoBehaviour 
{
  [SerializeField] private Character character = null;
  [SerializeField] private float distX = 0.4f;
  private BoxCollider boxCollider = null;

  private void Start()
  {
    boxCollider = GetComponent<BoxCollider>();
  }
  
  protected virtual void OnPress(bool isPressed)
  {
    if (isPressed)
    {
      if (Mathf.Abs(transform.position.x - character.transform.position.x) < distX && character.transform.position.y > transform.position.y - boxCollider.size.y / 2 && character.transform.position.y < transform.position.y + boxCollider.size.y / 2)
      {
        character.JumpToStair(transform.position.x - character.transform.position.x > 0);   //true - right; false - left
      }
    }
  }
}
