using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Monstr : MonoBehaviour
{
  [SerializeField] private AudioClip attack = null;
  [SerializeField] private AudioClip dead = null;
	
	public void Attack()
	{
	  audio.clip = attack;
	  audio.Play();
	}
}
