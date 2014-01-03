using System;
using UnityEngine;

public class ArmoGUI : MonoBehaviour
{
  [SerializeField] private Character character = null;//hide
  [SerializeField] private GameObject[] activeObjs = null;
  [SerializeField] private GameObject[] deactiveObjs = null;
  [SerializeField] private string oName = "";//Trigger name "AkmTrg" "PistolTrg" "AkmTrg"
  [SerializeField] private ThingTrigger[] thingTrigger = null;//hide
  [SerializeField] private UISprite empty = null;
  [SerializeField] private UISprite passiv = null;
  [SerializeField] private UISprite activ = null;
  [SerializeField] private UILabel counter = null;
  [SerializeField] private int patrons = 10;
  [SerializeField] private int id = 0;
  [SerializeField] private int state = 0;

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

  public UILabel Counter
  {
    get { return counter;}
  }

	private void Start ()
	{
	  
    character = GameObject.Find("Stalker").GetComponent<Character>();
    if (character.Patrons[id] > 0)
	    State = 1;
    if (character.CurrentArmo == id)
      State = 2;

    ThingTrigger[] thingTriggers2 = FindObjectsOfType(typeof(ThingTrigger)) as ThingTrigger[];
    int i = 0;
    foreach (ThingTrigger tt in thingTriggers2)
    {
      if (tt.gameObject.name == oName)
      {
        i += 1;
        Array.Resize(ref thingTrigger, i);
        thingTrigger[i - 1] = tt;
        tt.GetThing += GetThing;
      }
    }
    //Оружие, которое нужно деактивировать
	  if (id == 0)
      deactiveObjs = character.ArmoObjs;
	  i = 0;
    foreach (var daObj in deactiveObjs)
    {
      if (i != id-1)
        deactiveObjs[i] = character.ArmoObjs[i];
      i += 1;
    }

	  if (id != 0)
      activeObjs[1] = character.ArmoObjs[id-1];
    //Обновим счетчик патронов кроме кулака
    if (character.Patrons[id] > 0 && id > 0)
      counter.text = character.Patrons[id].ToString("f0");
	}

  private void Destroy()
  {
    foreach (var tht in thingTrigger)
    {
      tht.GetThing -= GetThing;
    }
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && Time.timeScale > 0.1f)
    {
      if (state == 1)
      {
        character.ResetArmo(false);//не все сбрасывать
          //Debug.LogWarning("ResetArmo");
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
    if (State == 0)
      State = 1;
    character.Patrons[id] += patrons;
    counter.text = character.Patrons[id].ToString("f0");
  }
}
