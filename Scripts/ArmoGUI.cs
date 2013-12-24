using UnityEngine;

public class ArmoGUI : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private GameObject[] activeObjs = null;
  [SerializeField] private GameObject[] deactiveObjs = null;
  [SerializeField] private ThingTrigger thingTrigger = null;
  [SerializeField] private UISprite empty = null;
  [SerializeField] private UISprite passiv = null;
  [SerializeField] private UISprite activ = null;
  [SerializeField] private int id = 0;
  [SerializeField] private int state = 0;
  [SerializeField] private bool presence = false;

  public bool Presence
  {
    get { return presence;}
    set 
    { 
      presence = value;
      if (presence && state == 0)
      {
        State = 1; 
      }
      if (!presence)
      {
        State = 0;
      }
    }
  }

  public int State
  {
    get { return state; }
    set
    {
      state = value;
      SetSprite();
    }
  }

  public GameObject[] ActiveObjs
  {
    get { return activeObjs; }
  }

  public GameObject[] DeactiveObjs
  {
    get { return deactiveObjs; }
  }

	private void Start ()
	{
	  SetSprite();
    if (thingTrigger!=null)
	    thingTrigger.GetThing += GetThing;
	}

  private void Destroy()
  {
    if (thingTrigger != null) 
      thingTrigger.GetThing -= GetThing;
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (presence)
      {
        if (state == 1)
        {
          character.ResetArmo();
          Debug.LogWarning("ResetArmo");
          State = 2;
          character.CurrentArmo = id;
          foreach (var aObjs in activeObjs)
          {
            aObjs.SetActiveRecursively(true);
          }

          foreach (var aObjs in deactiveObjs)
          {
            aObjs.SetActiveRecursively(false);
          }
          //выбрать оружие
        }
        //else
        //{
        //  State = 1;
        //  if (thing != null)
        //    thing.SetActive(false);
        //  //снять
        //}
      }
    }
  }
	
	void SetSprite () 
  {
    if (state == 0)
    {
      empty.enabled = true;
      passiv.enabled = false;
      activ.enabled = false;
    }
    if (state == 1)
    {
      empty.enabled = false;
      passiv.enabled = true;
      activ.enabled = false;
    }
    if (state == 2)
    {
      empty.enabled = false;
      passiv.enabled = false;
      activ.enabled = true;
    }
    if (state != 0 && state != 1 && state != 2)
      Debug.LogWarning("state no corect: " + state);
	}

  private void GetThing()
  {
    //State = 1;
    Presence = true;
  }
}
