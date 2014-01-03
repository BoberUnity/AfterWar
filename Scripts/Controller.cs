using UnityEngine;

public class Controller : MonoBehaviour
{
  //[SerializeField] private GameObject UIPrefab = null;
  [SerializeField] private int currentArmo = 0;
  [SerializeField] private int[] patrons = null;
  [SerializeField] private float helth = 100;
  [SerializeField] private int[] things = null;//Aptek, GazMask, Bron
  //private Character character = null;

  public int[] Patrons
  {
    get { return patrons; }
    set { patrons = value; }
  }

  public int CurrentArmo
  {
    set { currentArmo = value; }
  }

  public float Helth
  {
    set { helth = value; }
  }

  public int[] Things
  {
    set { things = value;}
  }

  private void Start()
  {
    DontDestroyOnLoad(this);
  }

  private void OnLevelWasLoaded(int level)
	{
    if (level > 1)
    {
      Character character = GameObject.Find("Stalker").GetComponent<Character>();
      character.Patrons = patrons;
      character.CurrentArmo = currentArmo;
      character.Helth = helth;
      character.Things = things;
    }
    if (level == 1)
    {
      Reset();
    }
	}

  public void Reset()
  {
    patrons[0] = 1000000;
    patrons[1] = 0;
    patrons[2] = 0;
    patrons[3] = 0;
    patrons[4] = 0;

    things[0] = 0;
    things[1] = 0;
    things[2] = 0;
    currentArmo = 0;
    helth = 100;
  }

  
}
