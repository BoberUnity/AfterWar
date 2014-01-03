using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LiftStarter : Button3DBase
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip up = null;
  [SerializeField] private AnimationClip down = null;
  [SerializeField] private Lamp lamp = null;
  private BoxCollider coll = null;
  private bool isUp = false;
  private bool move = false;

  private void Awake()
  {
    coll = GetComponent<BoxCollider>();
    character.TriggerEnter += DownStarter;
  }

  private void OnDestroy()
  {
    character.TriggerEnter -= DownStarter;
  }

  protected override void  MakeAction()
  {
    //Debug.LogWarning("MakeAction" + Time.time);
    if (!move && lamp.State == 2)
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
  //Активация лифта снизу
  private void DownStarter(string oName)
  {
    if (oName == "DownStarter")
    {
      MakeAction();
    }
  }
}
