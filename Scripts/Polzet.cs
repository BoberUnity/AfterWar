using UnityEngine;

public class Polzet : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private float y = 0;
  [SerializeField] private float minX = -100;
  [SerializeField] private float maxX = 100;
  private bool isUse = false;

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
    if (isUse)
    {
      if (characterTranform.position.y < y || characterTranform.position.x < minX || characterTranform.position.x > maxX)
      {
        character.Polzet = false;
        isUse = false;
      }
    }
	}
}
