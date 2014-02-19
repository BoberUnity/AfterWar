using System;
using System.Collections;
using Assets.Scripts.Buttons3D;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
  [Serializable] private class Block
  {
    [SerializeField] public PlayTwoAnims playTwoAnimses = null;
    [SerializeField] public bool key = false;
  }

  [SerializeField] private Animation doorAnim = null;
  [SerializeField] private Renderer lampRenderer = null;
  [SerializeField] private Material grayMat = null;
  [SerializeField] private Material redMat = null;
  [SerializeField] private Material greenMat = null;
  [SerializeField] private GameObject redFlare = null;
  [SerializeField] private GameObject greenFlare = null;
  [SerializeField] private Block[] blocks = null;
  private bool isOpen = false;

	private void Start () 
  {
    foreach (var but in blocks)
    {
      but.playTwoAnimses.Press += CheckKey;
    }
	}

  private void OnDestroy()
  {
    foreach (var but in blocks)
    {
      but.playTwoAnimses.Press -= CheckKey;
    }
  }
	
	private void CheckKey(int id, bool f)
	{
	  if (!isOpen)
    {
      bool right = true;

      foreach (var but in blocks)
	    {
        if (but.playTwoAnimses.Forward != but.key)
          right = false;
	    }
      if (right)
      {
        isOpen = true;
        doorAnim.Play();
        lampRenderer.material = greenMat;
        greenFlare.gameObject.SetActive(true);
      }
      else
      {
        lampRenderer.material = redMat;
        redFlare.gameObject.SetActive(true);
        StartCoroutine(ReturnMat(0.7f));
      }
    }
  }

  private IEnumerator ReturnMat(float time)
  {
    yield return new WaitForSeconds(time);
    lampRenderer.material = grayMat;
    redFlare.gameObject.SetActive(false);
  }
}
