using UnityEngine;

public class RigObject : MonoBehaviour
{
  private bool moveRight = false;

  public bool MoveRight
  {
    set { moveRight = value; }
  }

  private bool moveLeft = false;

  public bool MoveLeft
  {
    set { moveLeft = value; }
  }
  private void FixedUpdate()
  {
    if (moveRight)
      rigidbody.AddForce(0.03f, 0, 0);

    if (moveLeft)
      rigidbody.AddForce(-0.03f, 0, 0);
  }
}
