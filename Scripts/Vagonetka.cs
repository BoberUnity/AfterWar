using UnityEngine;

public class Vagonetka : MonoBehaviour
{
  [SerializeField] private float speed = 1;
  private float currSpeed = 0;
  private bool run = false;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
      run = true;
  }

  private void Update () 
  {
	  if (run)
	  {
	    if (currSpeed < speed)
        currSpeed += Time.deltaTime*0.3f;
      transform.Translate(currSpeed*Time.deltaTime,0,0);
	  }
	}
}
