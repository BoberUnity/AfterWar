using UnityEngine;

public class RigObject : MonoBehaviour
{
  [SerializeField] private Character character = null;
  private Transform characterTransform = null;
  [SerializeField]
  private bool moveRight = false;
  [SerializeField]
  private bool moveLeft = false;
  private Transform thisTransform = null;
  private float minY = 100;
  [SerializeField]
  private bool isNear = false;

  private void Awake()
  {
    thisTransform = transform;
    characterTransform = character.transform;
    minY = transform.position.y;
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
    //if (minY > transform.position.y)
    //  minY = transform.position.y;
    //transform.position = new Vector3(transform.position.x, minY, 0);
    //float distToChar = Vector3.Distance(thisTransform.position, characterTransform.position);
    //if (distToChar < 0.35f && characterTransform.position.y < thisTransform.position.y)
    //{
    //  thisTransform.parent = characterTransform;
    //  moveRight = thisTransform.position.x > characterTransform.position.x;
    //  moveLeft = thisTransform.position.x < characterTransform.position.x;
    //}
    //else
    //  thisTransform.parent = null;

    //if (character.Joystik.joysticValue.x < 10 && moveRight)
    //{
    //  moveRight = false;
    //  thisTransform.parent = null;
    //  transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    //}

    //if (character.Joystik.joysticValue.x > -10 && moveLeft)
    //{
    //  moveLeft = false;
    //  thisTransform.parent = null;
    //  transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    //}



    if (isNear)
    {
      float distToChar = Vector3.Distance(characterTransform.position, thisTransform.position);
      if (distToChar < 0.4f && characterTransform.position.y < thisTransform.position.y)
      {
        moveRight = thisTransform.position.x > characterTransform.position.x;
        moveLeft = thisTransform.position.x < characterTransform.position.x;
        character.MoveBoxAnim = true;
      }
      else
      {
        moveRight = false;
        moveLeft = false;
        character.MoveBoxAnim = false;
      }

      if (moveRight || moveLeft)
      {
        if (characterTransform.position.y < thisTransform.position.y)
        {
          if (moveRight)
            rigidbody.AddForce(200, 0, 0);

          if (moveLeft)
            rigidbody.AddForce(200, 0, 0);
        }
        else
        {
          character.MoveBoxAnim = false;
          moveRight = false;
          moveLeft = false;
          character.MoveBoxAnim = false;
        }
      }
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
      isNear = true;
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.name == "Stalker")
    {  
      isNear = false;
      moveRight = false;
      moveLeft = false;
      character.MoveBoxAnim = false;
    }
  }
}
