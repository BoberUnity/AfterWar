using UnityEngine;

public class HealthAdder : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private int val = 30;
	
	void OnEnable ()
	{
    character.Helth += val;
	  gameObject.SetActive(false);
	}
}
