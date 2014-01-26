using UnityEngine;

public class Vagonetka : MonoBehaviour
{
  [SerializeField] private float speed = 1;
  [SerializeField] private Transform hero = null;
  private float currSpeed = 0;
  private bool run = false;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
      run = true;
    if (other.gameObject.name == "Vrata")
    {
      run = false;
      currSpeed = 0;
      speed = 0;
    }

    if (other.gameObject.name == "Dyra")
    {
      //hero.parent = null;
      //rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
    }
  }

  private void Update () 
  {
	  if (run)
	  {
	    if (currSpeed < speed)
        currSpeed += Time.deltaTime*0.8f;
      transform.Translate(currSpeed*Time.deltaTime,0,0);
	  }
	}
}
