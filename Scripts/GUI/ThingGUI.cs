using System;
using System.Collections;
using UnityEngine;

public class ThingGUI : MonoBehaviour
{
  [SerializeField] private GameObject thing = null;
  [SerializeField] private GameObject thingDisable = null;
  [SerializeField] private string oName = "";//Trigger name "GazTrigger" "BronTrigger" "AptekTrigger"
  [SerializeField] private ThingTrigger[] thingTriggers = null;
  //[SerializeField] private ThingTrigger[] thingTriggers2 = null;
  [SerializeField] private UILabel counter = null;
  [SerializeField] private UISprite empty = null;
  [SerializeField] private UISprite passiv = null;
  [SerializeField] private UISprite activ = null;
  [SerializeField] private Indicator indicator = null;
  [SerializeField] private int id = 0;//0-apt 1-gazMask 2-bron
  [SerializeField] private int state = 0;
  //[SerializeField] private bool presence = false;
  [SerializeField] private float activeTime = 5;
  private Character character = null;
  //private int nums = 0;
  private float tUsed = 0;

  public int State
  {
    get { return state; }
    set
    {
      state = value;
      SetSprite();
    }
  }

  //=============================================================================================================
	private void Start ()
	{
    character = GameObject.Find("Stalker").GetComponent<Character>();
    if (id == 1)
    {
      thing = GameObject.Find("HeadGazMask");
      thingDisable = GameObject.Find("Head");
    }
    if (id == 2)
    {
      thing = GameObject.Find("BodyBron");
      thingDisable = GameObject.Find("Body");
    }

    if (character.Things[id] > 0)
    {
      State = 1;
      counter.text = character.Things[id].ToString("f0");
    }

    thing.SetActive(false);//????????????????????????????????????????? !!!!!!!!!!!!!!!!!!!
    if (thingDisable != null)
      thingDisable.SetActive(true);

    SetSprite();
    //if (thingTrigger!=null)
    //  thingTrigger.GetThing += GetThing;
    ThingTrigger[] thingTriggers2 = FindObjectsOfType(typeof(ThingTrigger)) as ThingTrigger[];
	  int i = 0;
    foreach (ThingTrigger tt in thingTriggers2)
    {
      if (tt.gameObject.name == oName)
      {
        i += 1;
        Array.Resize(ref thingTriggers, i);
        thingTriggers[i-1] = tt;
        tt.GetThing += GetThing;
      }
    }

    //foreach (var tTrig in thingTriggers)
    //{
    //  tTrig.GetThing += GetThing;
    //}
	}
  //=============================================================================================================
  private void Update()
  {
    if (state == 2)
    {
      tUsed += Time.deltaTime;
      if (indicator != null)
        indicator.Val = (activeTime - tUsed) * 100 / activeTime;
    }
  }
  //=============================================================================================================
  private void Destroy()
  {
    foreach (var tTrig in thingTriggers)
    {
      tTrig.GetThing -= GetThing;
    }
    //if (thingTrigger != null) 
    //  thingTrigger.GetThing -= GetThing;
  }
  //=============================================================================================================
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && Time.timeScale >0.1f)
    {
      //if (presence)
      //{
      if (state == 1)
      {
        //nums -= 1;
        if (tUsed < 0.0001f)
          character.Things[id] -= 1;
        else
          character.Things[id] -= indicator.Val / 100;

        counter.text = character.Things[id].ToString("f0");
        if (counter.text == "0")
          counter.text = "";
        State = 2;
        //tUsed = 0;
        if (thing != null)
          thing.SetActive(true);
        if (thingDisable != null)
          thingDisable.SetActive(false);
        //одеть
        if (id == 0)
          StartCoroutine(OffButton(activeTime - tUsed));
        if (id == 1)
          StartCoroutine(OffButton(activeTime - tUsed));//Время противогаза
      }
      else
      {
        if (state == 2)
        {
          character.Things[id] += indicator.Val/100;
          StopAllCoroutines();
          State = 1;
          if (thing != null)
            thing.SetActive(false);
          if (thingDisable != null)
            thingDisable.SetActive(true);
        }
      }
    }
  }
	//=============================================================================================================
  private IEnumerator OffButton(float time)
  {
    yield return new WaitForSeconds(time);
    thing.SetActive(false);
    tUsed = 0;
    //character.Things[id] -= 1;
    //counter.text = character.Things[id].ToString("f0");
    if (thingDisable != null)
      thingDisable.SetActive(true);
    if (character.Things[id] > 0.0001f)
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
    //Presence = true;//?
    if (State == 0)
      State = 1;
    character.Things[id] += 1;
    counter.text = character.Things[id].ToString("f0");
  }
}
