﻿using UnityEngine;

public class AnimSpeed : MonoBehaviour
{
  [SerializeField] private float animSpeed = 1;
	
	void Start ()
	{
	  GetComponent<Animation>()[GetComponent<Animation>().clip.name].speed = animSpeed;
	}
}
