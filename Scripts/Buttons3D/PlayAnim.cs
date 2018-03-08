using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class PlayAnim : Button3DBase
  {
    [SerializeField] private Animation anim = null;
    [SerializeField] private AnimationClip clip = null;
    [SerializeField] private bool always = false;
    [SerializeField] private AudioClip sound = null; 
    [SerializeField] private bool loop = false;
    [SerializeField] private ChangeState changeState = null;
    [SerializeField] private int necesarryState = 1;//до этого уже что-то нажато
    [SerializeField] private bool paused = false;//для записок
    private bool isPlayed = false;
    private AnimationClip mainClip = null;

    protected override void MakeAction()
    {
      if (anim.playAutomatically)
        mainClip = anim.clip;
      if ((changeState != null && changeState.State == necesarryState) || changeState == null)
      {
        if (!isPlayed)
        {
          if (clip != null)
            anim.clip = clip;
          anim.Play();
          PlayingSound();
          StartCoroutine(EndAnim(anim[anim.clip.name].length));
          if (!always)
            isPlayed = true;
          if (changeState != null)
            changeState.State += 1;
        }
      }
    }

    private IEnumerator EndAnim(float time)
    {
      yield return new WaitForSeconds(time);
      if (sound != null)
        GetComponent<AudioSource>().Stop();
      if (paused)
        Time.timeScale = 0;
      if (anim.playAutomatically)
      {
        anim.clip = mainClip;
        anim.Play();
      }
    }

    private void PlayingSound()
    {
      if (sound != null)
      {
        GetComponent<AudioSource>().clip = sound;
        if (character.Controller != null)
          GetComponent<AudioSource>().volume = character.Controller.EffectsVolume;
        GetComponent<AudioSource>().loop = loop;
        GetComponent<AudioSource>().Play();
        
      }
    }
  }
}
