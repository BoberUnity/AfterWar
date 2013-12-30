using UnityEngine;

public class PlayAnim : Button3DBase
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private bool always = false;
  private bool isPlayed = false;

  protected override void MakeAction()
  {
    if (!always && !isPlayed)
    {
      anim.Play();
      isPlayed = true;
    }

    if (always)
      anim.Play();
  }
}
