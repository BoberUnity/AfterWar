using UnityEngine;

public class HealthAdder : MonoBehaviour
{
  //[SerializeField] private Character character = null;
  [SerializeField] private int val = 30;
	
	void OnEnable ()
	{
    Character character = GameObject.Find("Stalker").GetComponent<Character>();
    character.Helth += val;
	  gameObject.SetActive(false);
	}
}
