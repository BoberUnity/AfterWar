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
  [SerializeField] private float activeTime = 5;
  [SerializeField] private float addHelth = 30;
  private Character character = null;
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

  public Indicator Indicator
  {
    get { return indicator; }
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
      counter.text = Mathf.Floor(character.Things[id]).ToString("f0");
    }
    //Debug.Log("id=" +id + " Things=" + character.Things[id] );
    //if (character.Things[id] - Mathf.Floor(character.Things[id]) > 0.01f)
    //{
    //  Debug.Log(character.Things[id] - Mathf.Floor(character.Things[id]));
    //  State = 2;
    //  counter.text = character.Things[id].ToString("f0");
    //  indicator.Val = character.Things[id] - Mathf.Floor(character.Things[id]);
    //}


    if (thing != null)
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
      if (id == 0 || id == 1)
      {
        tUsed += Time.deltaTime;
        if (indicator != null)
          indicator.Val = (activeTime - tUsed) * 100 / activeTime;
        if (id == 0)
        {
          character.Helth += Time.deltaTime / activeTime * addHelth;
        }
      }
      //counter.text = character.Things[id].ToString("f2");
      //character.Things[id] -= Time.deltaTime/activeTime;
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
    if (!isPressed && Time.timeScale > 0.1f)
    {
      if (state == 1)
      {
        if ((id == 0 || id == 1) && tUsed < 0.01f)
          character.Things[id] -= 1;
        else
          character.Things[id] -= indicator.Val/100;

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
        {
          StartCoroutine(OffButton(activeTime - tUsed));//Время противогаза
          character.UseGazMask = true;
        }
        if (id == 2)
        {
        //  //indicator.Val = 100;
          indicator.SetState(true);
        }
        //  StartCoroutine(OffButton(activeTime - tUsed));//Время bron
      }
      else
      {
        if (state == 2)
        {
          character.Things[id] += indicator.Val/100;
          StopAllCoroutines();
          State = 1;
          counter.text = Mathf.Floor(character.Things[id]).ToString("f0");
          if (counter.text == "0")
            counter.text = "";
          if (thing != null)
            thing.SetActive(false);
          if (thingDisable != null)
            thingDisable.SetActive(true);
          if (id == 1)
          {
            character.UseGazMask = false;
          }
          if (id == 2)
            indicator.SetState(false);
        }
      }
    }
  }
  //=============================================================================================================
  private void OnDisable()
  {
    if (state == 2)
    {
      StopAllCoroutines();
      State = 3;
    }
  }
  //=============================================================================================================
  private void OnEnable()
  {
    if (state == 3)
    {
      State = 2;

      //одеть
      if (id == 0)
        StartCoroutine(OffButton(activeTime - tUsed));
      if (id == 1)
      {
        StartCoroutine(OffButton(activeTime - tUsed));//Время противогаза
        character.UseGazMask = true;
      }
      if (id == 2)
      {
        indicator.SetState(true);
      }
    }
  }
	//=============================================================================================================
  public IEnumerator OffButton(float time)
  {
    yield return new WaitForSeconds(time);
    if (thing != null)
      thing.SetActive(false);
    tUsed = 0;
    //character.Things[id] -= 1;
    //counter.text = character.Things[id].ToString("f0");
    if (thingDisable != null)
      thingDisable.SetActive(true);
    if (character.Things[id] > 0.01f)
      State = 1;
    else
      State = 0;

    if (id == 1)
    {
      character.UseGazMask = false;
    }
    if (id == 2)
      indicator.SetState(false);
    //character.Things[id] -= 1;

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
    if (State == 0)
      State = 1;
    character.Things[id] += 1;
    counter.text = character.Things[id].ToString("f0");
  }
}
