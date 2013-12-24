using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  private void OnParticleCollision(GameObject other)
  {
    if (other.name == "Obj")
      Debug.Log("Obj");
    Destroy(other.gameObject);
    Debug.Log("Part");
  }
}
