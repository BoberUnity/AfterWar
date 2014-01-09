using UnityEngine;
using System.Collections;

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
      rigidbody.AddTorque(0, 0, -10);

    if (moveLeft)
      rigidbody.AddTorque(0, 0, 10);

  }
}
