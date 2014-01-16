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
    private bool isPlayed = false;

    protected override void MakeAction()
    {
      if (!isPlayed)
      {
        if (clip != null)
          anim.clip = clip;
        anim.Play();
        PlayingSound();
        if (!always)
          isPlayed = true;
      }
    }

    private IEnumerator EndAnim(float time)
    {
      yield return new WaitForSeconds(time);
      audio.Stop();
    }

    private void PlayingSound()
    {
      if (sound != null)
      {
        audio.clip = sound;
        if (character.Controller != null)
          audio.volume = character.Controller.EffectsVolume;
        audio.loop = loop;
        audio.Play();
        StartCoroutine(EndAnim(anim[anim.clip.name].length));
      }
    }
  }
}
