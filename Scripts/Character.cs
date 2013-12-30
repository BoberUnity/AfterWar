using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Character : MonoBehaviour
{
  [Serializable] private class Armo
  {
    [SerializeField] private AnimationClip armoAnim = null;
    [SerializeField] private AnimationClip armoRun = null;
    [SerializeField] private AnimationClip armoIdle = null;
    [SerializeField] private ParticleEmitter shootParticle = null;
    [SerializeField] private ArmoGUI armoGUI = null;
    [SerializeField] private AudioClip armoClip = null;
    //[SerializeField] private int patrons = 0;

    public AnimationClip ArmoAnim
    {
      get { return armoAnim; }
    }

    public AnimationClip ArmoRun
    {
      get { return armoRun; }
    }

    public AnimationClip ArmoIdle
    {
      get { return armoIdle; }
    }

    public ParticleEmitter ShootParticle
    {
      get { return shootParticle; }
    }

    public ArmoGUI ArmoGUI
    {
      get { return armoGUI; }
    }

    public AudioClip ArmoClip
    {
      get { return armoClip; }
    }

    //public int Patrons
    //{
    //  get { return patrons } ;
    //}
  }

  [SerializeField] private Armo[] armo = null;
  [SerializeField] private UIProgressBar progressBar = null;
  [SerializeField] private AnimationClip jumpClip = null;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AnimationClip stairClip = null;
  [SerializeField] private AnimationClip stairIdleClip = null;
  [SerializeField] private AnimationClip actionClip = null;
  [SerializeField] private UISprite deadSprite = null;
  [SerializeField] private Indicator helthIndicator = null;
  [SerializeField] private float curSpeed = 1;
  [SerializeField] private float rotSpeed = 580;
  [SerializeField] private float stairSpeed = 1;
  [SerializeField] private float gravSpeed = 2;
  [SerializeField] private float jumpHeight = 1;
  [SerializeField] private float helth = 80;
  [SerializeField] private int[] patrons = null;
  private float visotaDown = 1;
  private float visotaUp = 1;
  private bool liftZone;
  private float velocity = 0;
  private CharacterController characterController = null;
  private Transform t = null;
  [SerializeField]
  private bool jump = false;
  [SerializeField]
  private int jumpToStair = 0;//1 - right. 2 - left
  private bool kulak = false;
  private bool dead = false;
  private bool act = false;

  public event Action<string> TriggerEnter;
  public event Action<int> CharacterAttack;

  [SerializeField] private bool inStair = false;
  [SerializeField] private bool stairZone = false;
  [SerializeField] private bool enableSoskok = false;

  private int currentArmo = 0;

  public float Helth
  {
    get { return helth; }
    set 
    { 
      if (helth > 0)
      {
        helth = Mathf.Clamp(value, 0, 100);
        SetHelth();
      }
    }
  }

  public int CurrentArmo
  {
    get { return currentArmo; }
    set { currentArmo = value; }
  }

  public int[] Patrons
  {
    get { return patrons; }
    set { patrons = value; }
  }
  //==================================================================================================================
	void Start ()
	{
	  Application.targetFrameRate = 300;
	  characterController = GetComponent<CharacterController>();
	  t = transform;
	  patrons[0] = 10000000;
	}

  //==================================================================================================================
  void Update ()
  {
    // ЛУЧИ -------------------------------
    RaycastHit[] hits;
    hits = Physics.RaycastAll(t.position + Vector3.up * 0.3f, -Vector2.up, 100);
    int i = 0;
    visotaDown = 100;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      visotaDown = Mathf.Min(hit.distance, visotaDown);
      i++;
    }
    //луч вверх (придавило лифтом)
    hits = Physics.RaycastAll(t.position + Vector3.up * 0.3f, Vector2.up, 100);
    i = 0;
    visotaUp = 100;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      visotaUp = Mathf.Min(hit.distance, visotaUp);
      i++;
    }
    if (visotaUp < 0.12f)
    {
      deadSprite.enabled = true;
      Debug.LogWarning("Pridavilo visotaUp = " + visotaUp);
      //Time.timeScale = 0;
    }

    //ГРАВИТАЦИЯ -----------------------------------
    if (!inStair)
    {
      if (characterController.isGrounded && !jump)
      {
        if (velocity > 2)
          Helth -= 10;
        velocity = 0;
      }
      else
      {
        characterController.Move(-Vector3.up * Time.deltaTime * velocity);
        velocity += Time.deltaTime * gravSpeed;
        if (jump && stairZone && !inStair)
        {
          inStair = true;
          jump = false;//?
        }
      }
    }
    //ДВИЖЕНИЕ--------------------------
    if (Mathf.Abs(t.localPosition.z) > 0.02)
      t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);

    if (!dead && !inStair && !act && jumpToStair == 0)
    {
      if (Mathf.Abs(progressBar.joysticValue.x) > 30)
      {
        float step = progressBar.joysticValue.x*0.01f*curSpeed;
        characterController.Move(Vector3.right*Time.deltaTime*step);
        if (!jump && !kulak)
        {
          SetAnimCross(armo[currentArmo].ArmoRun, Mathf.Abs(step));
        }

        if (progressBar.joysticValue.x > 0)
        {
          if (t.eulerAngles.y > 90)
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(90, t.eulerAngles.y - rotSpeed*Time.deltaTime), t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(90, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
        }

        if (progressBar.joysticValue.x < 0)
        {
          if (t.eulerAngles.y < 270 && t.eulerAngles.y > 70)
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(270,t.eulerAngles.y + rotSpeed*Time.deltaTime), t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(270,t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z);
        }
      }
      else
      {
        if (!kulak)
          SetAnimCross(armo[currentArmo].ArmoIdle, 1);
      }
    }
    //НА ЛЕСТНИЦЕ----------------------
    if (inStair)
    {
      if (visotaDown < 0.31f && !jump && enableSoskok)//соскок с лестницы возле пола
      {
        inStair = false;
        Debug.Log("visotaDown < 0.31f"+Time.deltaTime);
        enableSoskok = false;
      }
      if (visotaDown > 0.31f)
        enableSoskok = true;

      if (jump && inStair && visotaDown < 0.31f)//немножко залезть вверх
      {
        characterController.Move(Vector3.up * Time.deltaTime * stairSpeed);
      }

      if (Mathf.Abs(progressBar.joysticValue.y) > 20)
      {
        float step = progressBar.joysticValue.y * 0.01f * stairSpeed;
        characterController.Move(Vector3.up * Time.deltaTime * step);
        SetAnimCross(stairClip, Mathf.Abs(step)*1.5f);
      }
      else
      {
        SetAnimCross(stairIdleClip, 0.001f);
      }
      t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y / (1 + rotSpeed) * Time.deltaTime * 0.01f, t.eulerAngles.z);
      if (Mathf.Abs(progressBar.joysticValue.x) > 99)
      {
        inStair = false;
        enableSoskok = false;
      }
    }
    //ИДЁТ К ЛЕСТНИЦЕ --------------------------------------------
    if (jumpToStair > 0 && !inStair )
    {
      if (stairZone)
      {
        inStair = true;
        jumpToStair = 0;
      }

      if (jumpToStair == 1)
      {
        characterController.Move(Vector3.right * Time.deltaTime);
        if (t.eulerAngles.y > 180)
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(360, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
        else
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(0, t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z); 
      }
      
      if (jumpToStair == 2)
      {
        characterController.Move(-Vector3.right * Time.deltaTime);
        if (t.eulerAngles.y > 180)
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(360, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
        else
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(0, t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z);
      }

      if (!jump && !kulak)
      {
        SetAnimCross(armo[currentArmo].ArmoRun, 1);
      }
    }

    //lift Zona
    if (liftZone && !jump)
    {
      if (visotaDown < 0.31f)
        t.position += Vector3.up * (0.31f - visotaDown);
      //if (visotaDown < 0.25f)
      //{
      //  t.localPosition += Vector3.up * (0.32f - visotaDown);
      //  Debug.LogWarning("Big correct height" + visotaDown);
      //}
      if (visotaDown > 0.32f && visotaDown < 0.35f)
        t.localPosition += Vector3.up * (0.32f - visotaDown);
    }

    //Поворот к стене во время действия
    if (act && t.eulerAngles.y < 125 && t.eulerAngles.y > 30)
    {
      t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y - rotSpeed*0.5f * Time.deltaTime, t.eulerAngles.z);
    }
        
    if (Input.GetKeyDown(KeyCode.Escape))
         Application.Quit();
    }
  //==================================================================================================================
  private void SetAnimCross(AnimationClip cl, float sp)
  {
    animation.clip = cl;
    animation[cl.name].speed = sp;
    animation.CrossFade(cl.name);
  }
  //==================================================================================================================
  private void SetAnimOnce(AnimationClip cl, float sp)
  {
    animation.clip = cl;
    animation[cl.name].speed = sp;
    animation.Play(cl.name);
  }
  
  //==================================================================================================================
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 10)
      liftZone = true;
    if (other.gameObject.layer == 11)
    {
      stairZone = true;
    }

    if (other.gameObject.layer == 12)//Action
    {
      act = true;
      SetAnimOnce(actionClip, 0.5f);
      StartCoroutine(ActOff(actionClip.length/*, other*/));
    }

    if (other.gameObject.layer == 13)//Event
    {
      var handler = TriggerEnter;
      if (handler != null)
        handler(other.gameObject.name);
    }

    //if (other.gameObject.name == "Rat")
    //{
    //  Helth -= 10;
    //  Monstr m = other.GetComponent<Monstr>();
    //  if (m != null)
    //    m.Attack();
    //  else
    //    Debug.LogWarning("Monstr has no component");
    //}
  }
  //==================================================================================================================
  public void Action()
  {
    act = true;
    SetAnimOnce(actionClip, 0.5f);
    StartCoroutine(ActOff(actionClip.length));
  }
  //==================================================================================================================
  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer == 10)
      liftZone = false;
    if (other.gameObject.layer == 11)
    {
      stairZone = false;
      inStair = false;
    }
  }
  //==================================================================================================================
  public void Jump()
  {
    if (characterController.isGrounded && !jump && !dead)
    {
      velocity = -jumpHeight;
      jump = true;
      SetAnimOnce(jumpClip, 0.4f);
      StartCoroutine(EndJump(jumpClip.length));
    }
    if (stairZone)
      inStair = !inStair;
  }

  public void JumpToStair(bool right)
  {
    if (characterController.isGrounded && !jump)
    {
      if (right)
        jumpToStair = 1;
      else 
        jumpToStair = 2;
    }
  }
  //==================================================================================================================
  private IEnumerator EndJump(float time)
  {
    yield return new WaitForSeconds(time);
    jump = false;
    progressBar.joysticValue.y = 0;
  }
  //==================================================================================================================
  public void Attack()
  {
    if (!kulak && Patrons[currentArmo] > 0 && !dead)
    {
      Patrons[currentArmo] -= 1;
      if (Patrons[currentArmo] < 1 || currentArmo == 0)
        armo[currentArmo].ArmoGUI.Counter.text = "";
      else
        armo[currentArmo].ArmoGUI.Counter.text = Patrons[currentArmo].ToString("f0");
      kulak = true;
      SetAnimOnce(armo[currentArmo].ArmoAnim, 0.7f);
      armo[currentArmo].ShootParticle.emit = true;
      audio.clip = armo[currentArmo].ArmoClip;
      audio.Play();
      StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length));

      var handler = CharacterAttack;
      if (handler != null)
        handler(currentArmo);

      while (Patrons[currentArmo] < 1)
      {
        armo[currentArmo].ArmoGUI.State = 0;
        ResetArmo(true);
        CurrentArmo -= 1;
        if (armo[currentArmo].ArmoGUI.State == 1)
        {
          armo[currentArmo].ArmoGUI.State = 2;

          foreach (var a in armo[currentArmo].ArmoGUI.ActiveObjs)
          {
            a.SetActiveRecursively(true);
          }
        }
      }
    }
  }
  //=================================================================================================================
  private IEnumerator RestartArmo(float time)
  {
    yield return new WaitForSeconds(time);
    if (kulak)
    {
      Patrons[currentArmo] -= 1;
      if (Patrons[currentArmo] < 1 || currentArmo == 0)
        armo[currentArmo].ArmoGUI.Counter.text = "";
      else
        armo[currentArmo].ArmoGUI.Counter.text = Patrons[currentArmo].ToString("f0");

      SetAnimOnce(armo[currentArmo].ArmoAnim, 0.7f);
      
      audio.clip = armo[currentArmo].ArmoClip;
      audio.Play();
      StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length));

      var handler = CharacterAttack;
      if (handler != null)
        handler(currentArmo);

      while (Patrons[currentArmo] < 1)
      {
        armo[currentArmo].ArmoGUI.State = 0;
        ResetArmo(true);
        armo[currentArmo].ShootParticle.emit = false;
        CurrentArmo -= 1;
        armo[currentArmo].ShootParticle.emit = true;
        if (armo[currentArmo].ArmoGUI.State == 1)
        {
          armo[currentArmo].ArmoGUI.State = 2;

          foreach (var a in armo[currentArmo].ArmoGUI.ActiveObjs)
          {
            a.SetActiveRecursively(true);
          }
        }
      }
    }
  }
  //==================================================================================================================
  public void EndAttack()
  {
    kulak = false;
    armo[currentArmo].ShootParticle.emit = false;
  }
  //==================================================================================================================
  public void ResetArmo(bool all)//true - deactive all
  {
    foreach (var a in armo)
    {
      if (!all)
        foreach (var aObjs in a.ArmoGUI.ActiveObjs)
        {
          aObjs.SetActiveRecursively(true);
        }

      foreach (var daObjs in a.ArmoGUI.DeactiveObjs)
      {
        daObjs.SetActiveRecursively(false);
      }

      if (a.ArmoGUI.State == 2)
        a.ArmoGUI.State = 1;
    }
  }
  ////==================================================================================================================

  private IEnumerator ActOff(float time)
  {
    yield return new WaitForSeconds(time);
    act = false;
  }
  //==================================================================================================================
  private void SetHelth()
  {
    helthIndicator.Val = helth;
    if (helth < 1)
    {  
      StopAllCoroutines();
      SetAnimOnce(deadClip, 0.5f);
      dead = true;
      deadSprite.enabled = true;
    }
  }
  //==================================================================================================================
  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawRay(transform.position + Vector3.up * 0.3f, -Vector3.up * visotaDown);
    Gizmos.color = Color.green;
    Gizmos.DrawRay(transform.position + Vector3.up*0.3f, Vector3.up*visotaUp);
  }
  //==================================================================================================================
}
