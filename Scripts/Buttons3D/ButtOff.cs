using UnityEngine;
using System.Collections;

public class ButtOff : MonoBehaviour
{
  [SerializeField] private UIButton uiButton = null;
  
  void OnEnable ()
  {
    uiButton.isEnabled = false;
  }
	
	// Update is called once per frame
	void Update () {
	
	}
}
