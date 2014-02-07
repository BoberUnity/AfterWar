using UnityEngine;

public class RatGenerator : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private GameObject ratPrefab = null;
  private float notVagonetkaTime = -3;
  private Transform characterT = null;
  private bool on = false;
	
	private void Start()
	{
	  characterT = character.transform;
	}
  
  void Update () 
  {
    if (characterT.parent == null)
    {
      if (on)
      {
        notVagonetkaTime += Time.deltaTime;
        if (notVagonetkaTime > 5 && character.Helth > 0)
        {
          Instantiate(ratPrefab, new Vector3(characterT.position.x - 2, 0.1f, Random.value*0.4f-0.2f), Quaternion.identity);
           notVagonetkaTime = 0;
        }
      }
    }
    else
    {
      notVagonetkaTime = 0;
      on = true;//Заскочил на вагонетку 1 раз
    }
	}
}
