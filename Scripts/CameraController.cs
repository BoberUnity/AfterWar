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
  [SerializeField] private float camHeight = 0.3f;
  [SerializeField] private float minX = -100;
  [SerializeField] private float maxX = 100;
  [SerializeField] private float minY = -100;
  [SerializeField] private float maxY = 100;
  [SerializeField] private float backX = 0;
  [SerializeField] private bool follow = true;
  [SerializeField]
  private float distL = 10;
  [SerializeField]
  private float distR = 10;

  private bool stop = false;
  private bool stopR = false;

  public Transform CamTrans
  {
    get { return cam; }
  }

  public float MinX
  {
    set { minX = value;}
  }

  public float MaxX
  {
    set { maxX = value; }
  }

  public float MinY
  {
    set { minY = value; }
  }

  public float CamDist
  {
    set { camDist = value; }
  }

  public float MinDist
  {
    set { minDist = value; }
  }

  public float MaxDist
  {
    set { maxDist = value; }
  }

  public float MaxY
  {
    set { maxY = value; }
  }

  public float CamHeight
  {
    set { camHeight = value; }
  }

  public bool Follow
  {
    set { follow = value; }
  }

	void Start () 
  {
    cam.parent = null; 
    fonar.parent = null;
	}

	void Update () 
  {
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

    RaycastHit[] hits;
    hits = Physics.RaycastAll(cam.position - Vector3.right*0.2f, Vector3.right, 0.2f);
    int i = 0;
    distL = 100;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      distL = Mathf.Min(hit.distance, distL);
      i++;
    }
    distL = 0.2f - distL;

    hits = Physics.RaycastAll(cam.position + Vector3.right * 0.2f, -Vector3.right, 0.2f);
    i = 0;
    distR = 100;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      distR = Mathf.Min(hit.distance, distR);
      i++;
    }
	  distR = 0.2f - distR;
    
    if (distL > 0)
    {
      stop = true;
    }
	  float raznicaX = cam.position.x - transform.position.x;
    if (raznicaX < 0)
    {
      stop = false;
    }
    if (raznicaX > 0.4f)
    {
      cam.position -= Vector3.right*0.4f;
      stop = false;
    }
    if (distR > 0)
    {
      stopR = true;
    }

    if (raznicaX > 0)
    {
      stopR = false;
    }
    if (raznicaX < -0.4f)
    {
      cam.position += Vector3.right * 0.4f;
      stopR = false;
    }


    //if (distR < 0.3f)
    //  backX = -(0.3f - distR);
    //if (distL > 0.3f && distR > 0.3f)
    //  backX = 0;
    if (!stop && !stopR)
    {
      plrPos = new Vector3(transform.position.x - backX, transform.position.y + camHeight, -camDist);
      if (follow)
        cam.forward = Vector3.Lerp(cam.forward, transform.position - cam.position, Time.deltaTime * camSpeed);
      cam.position = Vector3.Lerp(cam.position, plrPos, Time.deltaTime * camSpeed);
      cam.position = new Vector3(Mathf.Clamp(cam.position.x, minX, maxX), Mathf.Clamp(cam.position.y, minY, maxY), cam.position.z);
      fonar.position = new Vector3(transform.position.x, transform.position.y + fonarHeight, -fonarDist);
    }
	}

  //void OnDrawGizmos()
  //{
  //  Gizmos.color = Color.yellow;
  //  Gizmos.DrawRay(transform.position + Vector3.right * 0.5f, -Vector3.right * distL);
  //  Gizmos.color = Color.green;
  //  Gizmos.DrawRay(transform.position + Vector3.right * 0.5f, -Vector3.right * distL);
  //}
}
