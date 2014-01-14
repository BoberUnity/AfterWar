using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class AnimOnTrigger : MonoBehaviour
{
  [SerializeField] private Animation anim = null;

	private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
      anim.Play();
	}
}
