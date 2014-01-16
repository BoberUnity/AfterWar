using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GUI
{
  public class ButtonZapiskaOff : MonoBehaviour
  {
    [SerializeField] private AnimationClip backClip = null;
    protected virtual void OnPress(bool isPressed)
    {
      if (!isPressed)
      {
        animation.clip = backClip;
        //animation[animation.clip.name].speed = -1;
        animation.Play();
        //StartCoroutine(StopAnim(2));
      }
    }

    //private IEnumerator StopAnim(float time)
    //{
    //  yield return new WaitForSeconds(time);
    //  animation.Stop();
    //  //animation[animation.clip.name].speed = 1;
    //}
  }
}
