using UnityEngine;

public class Vagonetka : MonoBehaviour
{
  [SerializeField] private float speed = 1;
  [SerializeField] private Transform hero = null;
  private float currSpeed = 0;
  [SerializeField]
  private bool run = false;
  private bool dead = false;

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
      dead = true;
    }
  }

  private void Update () 
  {
	  if (run && !dead)
	  {
	    if (currSpeed < speed)
        currSpeed += Time.deltaTime*0.99f;
      transform.Translate(currSpeed*Time.deltaTime,0,0);
	  }
	}
}
