using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class PlatformDowner : MonoBehaviour
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip forwardClip = null;
  [SerializeField] private AnimationClip backClip = null;
  //private bool isPlaying = false;

	private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker"/* && !isPlaying*/)
    {
      //isPlaying = true;
      anim.clip = forwardClip;
      anim.Play();
      StartCoroutine(Finish(anim.clip.length));
    }
	}

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.name == "Stalker"/* && !isPlaying*/)
    {
      //isPlaying = true;
      anim.clip = backClip;
      anim.Play();
      StartCoroutine(Finish(anim.clip.length));
    }
  }
  //
  private IEnumerator Finish(float time)
  {
    yield return new WaitForSeconds(time);
    //isPlaying = false;
  }
}
