using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Vagonetka : MonoBehaviour
{
  [SerializeField] private float speed = 1;
  [SerializeField] private Transform stalker = null;
  //[SerializeField] private BoxCollider boxCollider = null;
  [SerializeField] private AudioClip crash = null;
  [SerializeField] private GameObject kolesoR1 = null;
  [SerializeField] private GameObject kolesoR2 = null;
  [SerializeField] private GameObject kolesoL1 = null;
  [SerializeField] private GameObject kolesoL2 = null;
  private float currSpeed = 0;
  private bool run = false;
  private bool dead = false;

  

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
      run = true;
    if (other.gameObject.name == "Vrata" || other.gameObject.name == "BochkaBenz")
    {
      Crash();
    }

    if (other.gameObject.name == "Dyra" && !dead)
    {
      //hero.parent = null;
      //rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
      dead = true;
      audio.clip = crash;
      audio.Play();
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

  private void Crash()
  {
    run = false;
    currSpeed = 0;
    speed = 0;
    stalker.parent = null;
    stalker.rigidbody.AddForce(20000,10000,0);
    transform.position = new Vector3(transform.position.x, 0.17f, transform.position.z);
    kolesoR1.AddComponent("BoxCollider");
    kolesoR1.AddComponent("Rigidbody");
    kolesoR1.rigidbody.AddForce(50, 50, -50);
    kolesoR2.AddComponent("BoxCollider");
    kolesoR2.AddComponent("Rigidbody");
    kolesoR2.rigidbody.AddForce(50, 50, -40);
    kolesoL1.AddComponent("BoxCollider");
    kolesoL1.AddComponent("Rigidbody");
    kolesoL1.rigidbody.AddForce(50, 50, 40);
    kolesoL2.AddComponent("BoxCollider");
    kolesoL2.AddComponent("Rigidbody");
    kolesoL2.rigidbody.AddForce(50, 50, 30);
    audio.clip = crash;
    audio.Play();
  }
}
