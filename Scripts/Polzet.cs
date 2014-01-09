using UnityEngine;

public class Polzet : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private float y = 0;
  [SerializeField] private bool isUse = false;

  public bool IsUse
  {
    set { isUse = value; }
  }
  private Transform characterTranform = null;

	void Start ()
	{
	  characterTranform = character.transform;
	}
	
	// Update is called once per frame
	void Update () 
  {
    if (characterTranform.position.y < y && isUse)
    {
      character.Polzet = false;
      isUse = false;
    }
	}
}
