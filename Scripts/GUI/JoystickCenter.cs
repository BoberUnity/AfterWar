using UnityEngine;

public class JoystickCenter : MonoBehaviour
{

  private Transform t = null;
  private UIProgressBar progressbar = null;
  
	void Start ()
	{
	  t = transform;
	  progressbar = t.parent.GetComponent<UIProgressBar>();
	}
	
	
	void Update () 
  {
    t.localPosition = new Vector3(progressbar.joysticValue.x*0.5f+64, progressbar.joysticValue.y*0.5f-64, t.localPosition.z);
	}
}
