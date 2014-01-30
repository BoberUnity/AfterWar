using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Vagonetka : MonoBehaviour
{
  [SerializeField] private float speed = 1;
  [SerializeField] private float dynamic = 0.5f;
  [SerializeField] private Character stalker = null;
  [SerializeField] private AudioSource audioSourceOnce = null;
  [SerializeField] private AudioSource audioSourceLoop = null;
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
    if (other.gameObject.name == "Stalker" && !run && !dead)
    {
      run = true;
      audioSourceLoop.Play();
    }
    if (other.gameObject.name == "Vrata" || other.gameObject.name == "BochkaBenz")
    {
      //Crash();
      currSpeed = currSpeed/2;// Mathf.Max(1, currSpeed - 2);
      stalker.Helth -= 10;
      DestroyedObject destObj = other.gameObject.GetComponent<DestroyedObject>();
      if (destObj != null)
        destObj.Crash();
      audioSourceOnce.clip = crash;
      audioSourceOnce.loop = false;
      audioSourceOnce.Play();
    }

    if (other.gameObject.name == "Dyra" && !dead)
    {
      dead = true;
      Crash();
    }
  }

  private void Update () 
  {
	  if (run && !dead)
	  {
	    if (currSpeed < speed)
        currSpeed += Time.deltaTime * dynamic;
      transform.Translate(currSpeed*Time.deltaTime,0,0);
	  }
	}

  private void Crash()
  {
    run = false;
    //currSpeed = 0;
    //speed = 0;
    stalker.transform.parent = null;
    stalker.VagSpeed = currSpeed;

    transform.position = new Vector3(transform.position.x, 0.18f, transform.position.z);
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
    audioSourceLoop.Stop();
    audioSourceOnce.clip = crash;
    audioSourceOnce.loop = false;
    audioSourceOnce.Play();
  }
}
