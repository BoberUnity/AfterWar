using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class AnimOnTrigger : MonoBehaviour
{
  [SerializeField] private Animation anim = null;
  private bool played = false;

	private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker" && !played)
    {
      anim.Play();
      played = true;
    }
    if (other.gameObject.name == "Stalker" && played)
    {
      //animation[animation.clip.name].time = 1;
      animation[animation.clip.name].speed = -1;
      anim.Play();
      played = false;
    }
	}
  //
  //private void OnTriggerExit(Collider other)
  //{
  //  if (other.gameObject.name == "Stalker")
  //    anim.Stop();
  //}
}
