using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  [RequireComponent(typeof(AudioSource))]
  public class SoundOn : Button3DBase
  {
    [SerializeField] private AudioClip sound = null;
    [SerializeField] private bool loop = false;

    protected override void MakeAction()
    {
      audio.clip = sound;
      audio.loop = loop;
      audio.Play();
    }
  }
}
