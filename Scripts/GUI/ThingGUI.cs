using System.Collections;
using UnityEngine;

public class ThingGUI : MonoBehaviour
{
  [SerializeField] private GameObject thing = null;
  [SerializeField] private ThingTrigger[] thingTriggers = null;
  [SerializeField] private UILabel counter = null;
  [SerializeField] private UISprite empty = null;
  [SerializeField] private UISprite passiv = null;
  [SerializeField] private UISprite activ = null;
  [SerializeField] private int id = 0;//0-apt 1-gazMask 2-bron
  [SerializeField] private int state = 0;
  [SerializeField] private bool presence = false;
  private int nums = 0;

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

	private void Start ()
	{
	  SetSprite();
    foreach (var tTrig in thingTriggers)
    {
      tTrig.GetThing += GetThing;
    }
    //if (thingTrigger!=null)
    //  thingTrigger.GetThing += GetThing;
	}

  private void Destroy()
  {
    foreach (var tTrig in thingTriggers)
    {
      tTrig.GetThing -= GetThing;
    }
    //if (thingTrigger != null) 
    //  thingTrigger.GetThing -= GetThing;
  }

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (presence)
      {
        if (state == 1)
        {
          nums -= 1;
          counter.text = nums.ToString("f0");
          if (counter.text == "0")
            counter.text = "";
          State = 2;
          if (thing != null)
            thing.SetActive(true);
          //одеть
          if (id == 0)
            StartCoroutine(OffButton(1.5f));
          if (id == 1)
            StartCoroutine(OffButton(6));//Время противогаза
        }
      }
    }
  }
	//=============================================================================================================
  private IEnumerator OffButton(float time)
  {
    yield return new WaitForSeconds(time);
    thing.SetActive(false);
    if (nums > 0)
      State = 1;
    else
      State = 0;
  }
  //=============================================================================================================
	private void SetSprite () 
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
    nums += 1;
    counter.text = nums.ToString("f0");
  }
}
