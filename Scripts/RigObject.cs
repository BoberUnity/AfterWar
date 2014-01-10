using UnityEngine;

public class RigObject : MonoBehaviour
{
  [SerializeField] private Character character = null;
  private Transform characterTransform = null;
  private bool moveRight = false;
  private bool moveLeft = false;
  private Transform thisTransform = null;

  private void Awake()
  {
    thisTransform = transform;
    characterTransform = character.transform;
  }

  public bool MoveRight
  {
    set { moveRight = value; }
  }

  public bool MoveLeft
  {
    set { moveLeft = value; }
  }

  private void FixedUpdate()
  {
    if (moveRight || moveLeft)
    {
      if (characterTransform.position.y < thisTransform.position.y)
      {
        if (moveRight)
          rigidbody.AddForce(0.022f, 0, 0);
      
        if (moveLeft)
          rigidbody.AddForce(-0.022f, 0, 0);
      }
      else
      {
        character.MoveBoxAnim = false;
        moveRight = false;
        moveLeft = false;
      }
    }
  }
}
