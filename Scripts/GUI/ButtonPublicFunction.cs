using UnityEngine;

public class ButtonPublicFunction : MonoBehaviour
{
  [SerializeField] private int id = 0;
  private Character character = null;

  private void Awake()
  {
    character = GameObject.Find("Stalker").GetComponent<Character>();
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (Time.timeScale > 0.1f)
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
        if (id == 0)
          character.EndJump();
        if (id == 1)
          character.EndAttack();
      }
    }
  }

  //PC
  private void Update()
  {
    if (id == 0)
    {
      if (Input.GetKeyDown(KeyCode.Return))
      {
        character.Attack();
      }
      if (Input.GetKeyUp(KeyCode.Return))
      {
        character.EndAttack();
      }
      if (Input.GetKeyDown(KeyCode.Space))
      {
        character.Jump();
      }
      if (Input.GetKeyUp(KeyCode.Space))
      {
        character.EndJump();
      }
    }
  }
}
