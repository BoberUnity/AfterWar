using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  [RequireComponent(typeof (AudioSource))]
  public class MoveBox : MonoBehaviour
  {
    [SerializeField] private Character character = null;
    [SerializeField] private float fixDist = 0.36f;
    //private float distToChar = 10;
    private bool isFix = false;
    private bool moveRight = false;
    private bool moveLeft = false;
    private Transform t = null;
    private Transform ct = null;

    private void Start()
    {
      t = transform;
      ct = character.transform;
      character.CharacterAttack += CharacterAttack;
      character.CharacterJump += CharacterJump;
    }

    private void OnDestroy()
    {
      character.CharacterAttack -= CharacterAttack;
      character.CharacterJump -= CharacterJump;
    }

    protected virtual void OnPress(bool isPressed)
    {
      if (!isPressed)
      {
        if (isFix)
        {
          ReleaseBox();
        }
        else
        {
          if (Vector3.Distance(t.position, ct.position) < fixDist && ct.position.y < t.position.y + 0.1f)
          {
            character.MoveBoxAnim = true;
            CorrectCharacterPosition();
            t.parent.parent = ct;
            isFix = true;
          }
        }
      }
    }

    private void Update()
    {
      
      if (isFix)
      {
        float distToChar = Vector3.Distance(transform.position, character.transform.position);
        if (distToChar > fixDist)
        {
          transform.parent.parent = null;
          isFix = false;
          character.MoveBoxAnim = false;
        }
        if (distToChar < fixDist - 0.015f)
        {
          ReleaseBox();
          Debug.Log("Release");
        }

        if (moveRight) 
        {
          character.MoveBoxBack = character.Joystik.joysticValue.x < 0;
        }
        if (moveLeft)
        {
          character.MoveBoxBack = character.Joystik.joysticValue.x > 0;
        }
      }
    }

    private void CorrectCharacterPosition()
    {
      float distToChar = Vector3.Distance(transform.position, character.transform.position);
      if (character.transform.position.x < transform.position.x) //ящик справа
      {
        character.transform.position -= Vector3.right*(fixDist - distToChar);
        moveRight = true;
      }
      else
      {
        character.transform.position += Vector3.right*(fixDist - distToChar);
        moveLeft = true;
      }
    }

    private void ReleaseBox()//Бросить ящик
    {
      transform.parent.parent = null;
      isFix = false;
      character.MoveBoxAnim = false;
    }

    private void CharacterAttack(int armo)
    {
      ReleaseBox();
    }

    private void CharacterJump()
    {
      ReleaseBox();
    }
  }
}
