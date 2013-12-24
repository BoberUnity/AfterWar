using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LiftStarter : Button3DBase
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip up = null;
  [SerializeField] private AnimationClip down = null;
  private BoxCollider coll = null;
  private bool isUp = false;
  private bool move = false;

  private void Awake()
  {
    coll = GetComponent<BoxCollider>();
  }

  protected override void  MakeAction()
  {
 	   if (!move)
     {
        if (isUp)
          anim.clip = down;
        else
          anim.clip = up;
        anim.Play();
        move = true;
        coll.enabled = false;
        StartCoroutine(StopLift(anim.clip.length));
     }
  }

  private IEnumerator StopLift(float time)
  {
    yield return new WaitForSeconds(time);
    coll.enabled = true;
    move = false;
    isUp = !isUp;
  }
}
