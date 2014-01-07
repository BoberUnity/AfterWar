using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
  [SerializeField] private float animSpeed = 1;
	
	void Start ()
	{
	  animation[animation.clip.name].speed = animSpeed;
	}
}
