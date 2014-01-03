using UnityEngine;
using System.Collections;

public class TouchCam : MonoBehaviour {

  private Touch myTouch; // прикосновение 1
  private Touch myTouch2; // прикосновение 1
  private float distance = 24;
  private float distanceOld = 0;
  [SerializeField] private UILabel Label = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
  {
	  if (Input.touchCount == 2)
	  {
      myTouch = Input.GetTouch(0);
      myTouch2 = Input.GetTouch(1);
      if (distanceOld == 0)
        distanceOld = distance;
      distance = Vector2.Distance(myTouch.position, myTouch2.position);
      

	    Label.text = distance.ToString();
	  }
	  else
	  {
	    distanceOld = 0;
	  }
	}
}
