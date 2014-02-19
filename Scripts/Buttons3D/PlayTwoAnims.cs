using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class PlayTwoAnims : Button3DBase
  {
    [SerializeField] private int id = 0;
    [SerializeField] private Animation anim = null;
    [SerializeField] private AnimationClip clipFirst = null;
    [SerializeField] private AnimationClip clipSecond = null;
    [SerializeField] private AudioClip sound = null;
    public event Action<int, bool> Press = null;

    private bool forward = true;
    public bool Forward
    {
      get { return forward;}
    }

    public int Id
    {
      get { return id;}
    }
    
    private bool isPlayed = false;

    protected override void MakeAction()
    {
      if (!isPlayed)
      {
        if (forward)
          anim.clip = clipFirst;
        else
          anim.clip = clipSecond;
        forward = !forward;
        anim.Play();
        PlayingSound();
        StartCoroutine(EndAnim(anim[anim.clip.name].length));
        
        isPlayed = true;
        var handled = Press;
        if (handled != null)
          handled(id, forward);
      }
    }

    private IEnumerator EndAnim(float time)
    {
      yield return new WaitForSeconds(time);
      if (sound != null)
        audio.Stop();
      isPlayed = false;
    }

    private void PlayingSound()
    {
      if (sound != null)
      {
        audio.clip = sound;
        if (character.Controller != null)
          audio.volume = character.Controller.EffectsVolume;
        //audio.loop = loop;
        audio.Play();
        
      }
    }
  }
}
