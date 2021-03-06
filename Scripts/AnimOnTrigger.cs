﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class AnimOnTrigger : MonoBehaviour
{
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip forwardClip = null;
  [SerializeField] private AnimationClip backClip = null;
  private bool isPlaying = false;
  private bool inEnd = false;

	private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker" && !isPlaying)
    {
      isPlaying = true;
      if (!inEnd)
        anim.clip = forwardClip;
      else
        anim.clip = backClip;
      anim.Play();
      StartCoroutine(Finish(anim.clip.length));
    }
    
	}
  //
  private IEnumerator Finish(float time)
  {
    yield return new WaitForSeconds(time);
    isPlaying = false;
    inEnd = !inEnd;
  }
}
