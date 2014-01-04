using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private Transform cam = null;
  [SerializeField] private Transform fonar = null;
  [SerializeField] private float minDist = 0.6f;
  [SerializeField] private float maxDist = 3.5f;
  [SerializeField] private float sensity = 0.002f;//чувств
  [SerializeField] private float camSpeed = 0;
  [SerializeField] private float fonarHeight = 0.5f;
  [SerializeField] private float fonarDist = 0.5f;
  [SerializeField] private float camDist = 2;
  private Touch myTouch; // прикосновение 1
  private Touch myTouch2; // прикосновение 1
  private float distance = 24;
  private float distanceStart = 0;
  private Vector3 plrPos;
  /*[SerializeField]*/ private float camHeight = 0.2f;

	void Start () 
  {
    cam.parent = null; 
    fonar.parent = null;
	}

	void Update () 
  {
    if (transform.position.y > 1f)
      camHeight = 0.05f;
    else
      camHeight = 0.55f;

	  if (Input.touchCount == 2 && Mathf.Abs(character.Joystik.joysticValue.x) < 2)
	  {
      myTouch = Input.GetTouch(0);
      myTouch2 = Input.GetTouch(1);
      distance = Vector2.Distance(myTouch.position, myTouch2.position);
      if (distanceStart < 1)
        distanceStart = distance;
      camDist += (distanceStart - distance) * sensity;
      camDist = Mathf.Clamp(camDist, minDist, maxDist);
	  }
	  else
	  {
	    distanceStart = 0;
	  }

    plrPos = new Vector3(transform.position.x, transform.position.y + camHeight, -camDist);
    cam.forward = Vector3.Lerp(cam.forward, transform.position - cam.position, Time.deltaTime * camSpeed);
    cam.position = Vector3.Lerp(cam.position, plrPos, Time.deltaTime * camSpeed);
    fonar.position = new Vector3(transform.position.x, transform.position.y + fonarHeight, -fonarDist);
	}
}
