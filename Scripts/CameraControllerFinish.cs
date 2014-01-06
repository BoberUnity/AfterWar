using UnityEngine;

public class CameraControllerFinish: MonoBehaviour
{
  [SerializeField] private CharacterFinish character = null;
  [SerializeField] private Transform cam = null;
  [SerializeField] private Transform fonar = null;
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
  [SerializeField] private bool follow = true;

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
    plrPos = new Vector3(transform.position.x, transform.position.y + camHeight, transform.position.z- camDist);
    if (follow)
      cam.forward = Vector3.Lerp(cam.forward, transform.position - cam.position, Time.deltaTime * camSpeed);
    cam.position = Vector3.Lerp(cam.position, plrPos, Time.deltaTime * camSpeed);
    cam.position = new Vector3(Mathf.Clamp(cam.position.x, minX, maxX), Mathf.Clamp(cam.position.y, minY, maxY), cam.position.z);
    fonar.position = new Vector3(transform.position.x, transform.position.y + fonarHeight, transform.position.z - fonarDist);
	}
}
