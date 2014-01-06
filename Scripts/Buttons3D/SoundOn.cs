using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  [RequireComponent(typeof(AudioSource))]
  public class SoundOn : Button3DBase
  {
    [SerializeField] private AudioClip sound = null;
    [SerializeField] private float distanse = 1.5f;
    [SerializeField] private float addVolume = 0.1f;//Компенсация, т.к. дистанция не м.б. 0
    [SerializeField] private bool loop = false;
    private Transform t = null;
    private Transform ct = null;

    protected override void MakeAction()
    {
      audio.clip = sound;
      audio.loop = loop;
      audio.Play();
    }

    private void Start()
    {
      t = transform;
      ct = character.transform;
    }

    private void Update()
    {
      audio.volume = (1 - Vector3.Distance(t.position, ct.position)/distanse + addVolume);
      if (character.Controller != null)
        audio.volume*=character.Controller.EffectsVolume;
      else
        Debug.LogWarning("character.Controller == null");
    }
  }
}
