using UnityEngine;

public class Controller : MonoBehaviour
{
  //[SerializeField] private GameObject UIPrefab = null;
  [SerializeField] private int currentArmo = 0;
  [SerializeField] private int[] patrons = null;
  [SerializeField] private float helth = 100;
  [SerializeField] private float[] things = null;//Aptek, GazMask, Bron
  [SerializeField] private bool showFPS = true;
  //private Character character = null;
  [SerializeField] private float effectsVolume = 0.7f;
  [SerializeField] private float musicVolume = 0.7f;
  [SerializeField] private float screenBright = 0.7f;
  [SerializeField] private bool waterHigh = false;

  public float EffectsVolume
  {
    get { return effectsVolume; }
    set { effectsVolume = value; }
  }

  public float MusicVolume
  {
    get { return musicVolume; }
    set { musicVolume = value; }
  }

  public bool WaterHigh
  {
    get { return waterHigh; }
    set { waterHigh = value; }
  }

  public float ScreenBright
  {
    get { return screenBright; }
    set { screenBright = value; }
  }

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

  public float[] Things
  {
    set { things = value;}
  }

  public bool ShowFps
  {
    set { showFPS = value;}
    get { return showFPS; }
  }

  private void Start()
  {
    DontDestroyOnLoad(this);
  }

  private void OnLevelWasLoaded(int level)
	{
    if (level > 1)
    {
      GameObject obj = GameObject.Find("Stalker");
      if (obj != null)
      {
        Character character = obj.GetComponent<Character>();
        character.Patrons = patrons;
        character.CurrentArmo = currentArmo;
        character.Helth = helth;
        character.Things = things;
        character.Controller = this;
      }
      else Debug.LogWarning("Stalker was not founded");
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
