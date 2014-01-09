using UnityEngine;

public class Polzet : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private float y = 0;
  private Transform characterTranform = null;
	void Start ()
	{
	  characterTranform = character.transform;
	}
	
	// Update is called once per frame
	void Update () 
  {
    if (characterTranform.position.y < y)
      character.Polzet = false;
	}
}
