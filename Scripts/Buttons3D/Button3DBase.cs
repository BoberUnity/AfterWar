using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Button3DBase : MonoBehaviour
{
  [SerializeField] protected Character character = null;
  [SerializeField] protected float dist = 0.1f;
  
  protected virtual void OnPress(bool isPressed)
  {
    if (Vector3.Distance(transform.position, character.transform.position) < dist)
    {
      character.Action();
      StartCoroutine(StartAction(0.5f));
    }
  }

  private IEnumerator StartAction(float time)
  {
    yield return new WaitForSeconds(time);
    MakeAction();
  }

  protected virtual void MakeAction()
  {
  }
}
