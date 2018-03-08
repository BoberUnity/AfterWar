using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class LiftStarter : Button3DBase
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip up = null;
  [SerializeField] private AnimationClip down = null;
  [SerializeField] private AudioClip sound = null;
  [SerializeField] private Lamp lamp = null;
  //[SerializeField] private Animation motorAnim = null;
  //[SerializeField] private Remen remen = null;
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
        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().loop = true;
        if (character.Controller != null)
          GetComponent<AudioSource>().volume = character.Controller.EffectsVolume;
        GetComponent<AudioSource>().Play();
        StartCoroutine(StopLift(anim.clip.length));
        //if (motorAnim != null)
        //  motorAnim.Play();
        //if (remen != null)
        //  remen.On = true;
     }
  }

  private IEnumerator StopLift(float time)
  {
    yield return new WaitForSeconds(time);
    coll.enabled = true;
    move = false;
    GetComponent<AudioSource>().Stop();
    //if (motorAnim != null)
    //  motorAnim.Stop();
    //if (remen != null)
    //  remen.On = false;
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
