using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Character : MonoBehaviour
{
  [Serializable] private class Armo
  {
    [SerializeField] private AnimationClip armoAnim = null;
    [SerializeField] private AnimationClip armoDownAnim = null;
    [SerializeField] private AnimationClip attackRun = null;
    [SerializeField] private AnimationClip armoRun = null;
    [SerializeField] private AnimationClip armoIdle = null;
    [SerializeField] private ParticleEmitter shootParticle = null;
    [SerializeField] private AudioClip armoClip = null;
    [SerializeField] private float animSpeed = 1;

    public AnimationClip ArmoAnim
    {
      get { return armoAnim; }
    }

    public AnimationClip ArmoDownAnim
    {
      get { return armoDownAnim; }
    }

    public AnimationClip AttackRun
    {
      get { return attackRun; }
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

    public AudioClip ArmoClip
    {
      get { return armoClip; }
    }

    public float AnimSpeed
    {
      get { return animSpeed;}
    }
  }

  [SerializeField] private Armo[] armo = null;
  /*[SerializeField]*/ private UIProgressBar progressBar = null;
  [SerializeField] private AnimationClip jumpClip = null;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AnimationClip deadBackClip = null;
  [SerializeField] private AnimationClip stairClip = null;
  [SerializeField] private AnimationClip stairIdleClip = null;
  [SerializeField] private AnimationClip actionClip = null;
  [SerializeField] private AnimationClip moveFwClip = null;
  [SerializeField] private AnimationClip moveBoxClip = null;
  [SerializeField] private AnimationClip moveBoxBackClip = null;
  [SerializeField] private AnimationClip swimClip = null;
  [SerializeField] private AnimationClip swimDeadClip = null;
  /*[SerializeField]*/ private UISprite deadSprite = null;
  /*[SerializeField]*/ private Indicator helthIndicator = null;
  [SerializeField] private AudioClip failSound = null;
  [SerializeField] private Controller controller = null;
  [SerializeField] private Transform hand = null;
  [SerializeField] private GameObject BlastPrefab = null;
  [SerializeField] private GameObject MolnyPrefab = null;
  [SerializeField] private Transform endPistol = null;
  [SerializeField] private GameObject PistolPartPrefab = null;
  [SerializeField] private float curSpeed = 1;
  [SerializeField] private float rotSpeed = 580;
  [SerializeField] private float stairSpeed = 1;
  [SerializeField] private float gravSpeed = 2;
  [SerializeField] private float jumpHeight = 1;
  [SerializeField] private float failHeight = 1.8f;
  private float helth = 100;
  [SerializeField] private int[] patrons = new int[5];
  [SerializeField] private ArmoGUI[] armosGUI = new ArmoGUI[5];
  [SerializeField] private GameObject[] armoObjs = new GameObject[4];
  [SerializeField] private float[] things = new float[3];//Aptek, GazMask, Bron
  [SerializeField] private float visotaDown = 1;
  private float visotaUp = 1;
  [SerializeField] private float velocity = 0;
  private CharacterController characterController = null;
  private Transform t = null;
  private bool jump = false;
  private int jumpToStair = 0;//1 - right. 2 - left
  private bool kulak = false;
  private bool dead = false;
  private bool act = false;
  public event Action<string> TriggerEnter;
  public event Action<int> CharacterAttack;
  public event Action CharacterJump;
  private bool inStair = false;
  private bool stairZone = false;
  private bool enableSoskok = false;
  private int currentArmo = 0;
  private int nearMonstr = 0;
  private bool fail = false;//падение
  private bool enableDead = false;
  private bool shooting = false;
  //[SerializeField] private bool isG;
  private float visotaShoot = 1;
  private bool polzet = false;
  private bool moveBox = false;
  private bool moveBoxAnim = false;//off after 0.1 sec
  private bool moveBoxBack = false;
  private ThingGUI bronButton = null;
  private bool isSwiming = false;
  private bool useGazMask = false;
  private bool inGazZone = false;
  
  public ThingGUI BronButton
  {
    set { bronButton = value;}
  }

  public int NearMonstr//Количество монстров близко
  {
    get { return nearMonstr; }
    set { nearMonstr = value; }
  }

  public float Helth
  {
    get { return helth; }
    set 
    {
      if (helth > 0)
      {
        if (bronButton.State != 2 || value > helth)
        {
          if (value < helth && enableDead)
          deadSprite.animation.Play();

          helth = Mathf.Clamp(value, 0, 100);
          SetHelth();
        }
        else
        {
          bronButton.Indicator.Val -= (helth - value)*2;//Bron live
          if (bronButton.Indicator.Val < 1)
          {
            StartCoroutine(bronButton.OffButton(0));
            bronButton.Indicator.Val = 100;
          }
          if (value < -101)
          {
            helth = 0;
            SetHelth();
          }
        }
      }
    }
  }

  public UIProgressBar Joystik
  {
    set { progressBar = value; }
    get { return progressBar; }
  }

  public UISprite DeadSprite
  {
    set { deadSprite = value; }
  }

  public Indicator HelthIndicator
  {
    set { helthIndicator = value; }
  }

  public int CurrentArmo
  {
    get { return currentArmo; }
    set 
    { 
      currentArmo = value; 
    }
  }

  public int[] Patrons
  {
    get { return patrons; }
    set { patrons = value; }
  }

  public ArmoGUI[] ArmosGUI
  {
    get { return armosGUI; }
    set { armosGUI = value;}
  }

  public GameObject[] ArmoObjs
  {
    get { return armoObjs;}
  }

  public float[] Things
  {
    get { return things; }
    set
    {
      things = value;
    }
  }

  public bool MoveBoxAnim
  {
    set { moveBoxAnim = value; }
  }

  public bool MoveBoxBack
  {
    set { moveBoxBack = value; }
  }

  public Controller Controller
  {
    get { return controller; }
    set { controller = value; }
  }

  public bool Polzet
  {
    get { return polzet; }
    set
    {
      polzet = value;
      if (polzet)
      {
        characterController.height = 0.54f;
        characterController.center = Vector3.up * 0.35f;
        //Debug.LogWarning("enter polaet-" + Time.time);
      }
      else
      {
        characterController.height = 1.913f;
        characterController.center = Vector3.up * 0.978f;
        //Debug.LogWarning("exit polzet-" + Time.time);
      }
    }
  }

  public bool UseGazMask
  {
    set
    {
      useGazMask = value;
      if (!useGazMask && inGazZone)
        Helth -= 301;
    }
  }
  //==================================================================================================================
	void Start ()
	{
	  Application.targetFrameRate = 30000;
	  characterController = GetComponent<CharacterController>();
	  t = transform;
	  patrons[0] = 10000000;
    if (currentArmo > 0)
      armoObjs[currentArmo - 1].gameObject.SetActive/*Recursively*/(true);
	  StartCoroutine(DeadSpriteOn(0.1f));
	}

  //==================================================================================================================
  void Update ()
  {
    //isG = characterController.isGrounded;
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
    //PRIDAVILO !!! DO NOT DESTROY !!!
    //if (visotaUp < 0.12f)
    //{
    //  deadSprite.enabled = true;
    //  Debug.LogWarning("Pridavilo visotaUp = " + visotaUp);
    //  //Time.timeScale = 0;
    //}
    //Контроллер падения
    if (visotaDown > failHeight && !fail)
      fail = true;
    
    //ГРАВИТАЦИЯ -----------------------------------
    if (!inStair)
    {
      if (characterController.isGrounded)
      {
        if (fail)//Падение
        {
          if (velocity > 2.4f)
          {
            Helth -= 10;
            if (controller != null)
              audio.volume = controller.EffectsVolume;
            audio.clip = failSound;
            audio.Play();
            velocity = 0.0f;
          }
          fail = false;
        }
        if (jump)
        {
          jump = false; 
          Jump();
        }
        else
          velocity = 0;
      }
      characterController.Move(-Vector3.up * Time.deltaTime * velocity);
      velocity = Mathf.Min(2.5f, velocity + Time.deltaTime * gravSpeed);
      if (polzet)//Гравитация в ползание
        velocity = Mathf.Min(0.4f, velocity + Time.deltaTime * gravSpeed);
    }
    else
    {
      velocity = 0;
    }
    //ДВИЖЕНИЕ--------------------------
    if (Mathf.Abs(t.localPosition.z) > 0.02)
      t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);

    if (!dead && !inStair && !act && jumpToStair == 0)
    {
      if (Mathf.Abs(progressBar.joysticValue.x) > 30)
      {
        float step = progressBar.joysticValue.x*0.01f*curSpeed;
        if (polzet || moveBoxAnim || isSwiming)
          step *= 0.25f;
        
        characterController.Move/*transform.position +=*/ (Vector3.right*Time.deltaTime*step);
        if (!jump && !kulak && !polzet && !moveBoxAnim && !isSwiming)
        {
          SetAnimCross(armo[currentArmo].ArmoRun, Mathf.Abs(step));
        }
        if (polzet)//Анимация ползания
          SetAnimCross(moveFwClip, Mathf.Abs(step*4));
        if (moveBoxAnim)//Анимация толкания
        {
          if (moveBoxBack)
            SetAnimCross(moveBoxBackClip, Mathf.Abs(step * 1.7f));
          else
            SetAnimCross(moveBoxClip, Mathf.Abs(step*1.7f));
        }
        if (isSwiming)
          SetAnimCross(swimClip, Mathf.Abs(step * 4));

        if (progressBar.joysticValue.x > 0 && !moveBoxAnim)
        {
          if (t.eulerAngles.y > 90)
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(90, t.eulerAngles.y - rotSpeed*Time.deltaTime), t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(90, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
        }

        if (progressBar.joysticValue.x < 0 && !moveBoxAnim)
        {
          if (t.eulerAngles.y < 270 && t.eulerAngles.y > 70)
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(270,t.eulerAngles.y + rotSpeed*Time.deltaTime), t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(270,t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z);
        }
      }
      else
      {
        if (!kulak && !jump && !shooting && !polzet)
          SetAnimCross(armo[currentArmo].ArmoIdle, 1);
        if (polzet)
          SetAnimCross(moveFwClip, 0.02f);//лежит ползя
        if (moveBoxAnim)
          SetAnimCross(moveBoxClip, 0);//толкает idle
        if (isSwiming)
          SetAnimCross(swimClip, 0.5f);
      }
    }
    //НА ЛЕСТНИЦЕ----------------------
    if (inStair)
    {
      if (visotaDown < 0.31f && !jump && enableSoskok && !polzet)//соскок с лестницы возле пола
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

    //Поворот к стене во время действия
    if (act && t.eulerAngles.y < 125 && t.eulerAngles.y > 30)
    {
      t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y - rotSpeed*0.5f * Time.deltaTime, t.eulerAngles.z);
    }
        
    if (Input.GetKeyDown(KeyCode.Escape))
         Application.Quit();
    }
  //==================================================================================================================
  
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
      t.parent = other.transform;
    
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

    if (other.gameObject.name == "Polzet" && !polzet)
    {
      Polzet = true;
      Polzet polzetComponent = other.gameObject.GetComponent<Polzet>();
      if (polzetComponent != null)
      {
        polzetComponent.IsUse = true;
      }
    }

    if (other.gameObject.name == "WaterDead")
    {
      Helth -= 301;
      GameObject molny = Instantiate(MolnyPrefab, transform.position, Quaternion.identity) as GameObject;
      if (molny != null)
        molny.transform.parent = transform;
    }

    if (other.gameObject.name == "GazZona")
    {
      inGazZone = true;
      if (!useGazMask)
        Helth -= 301;
    }

    if (other.gameObject.name == "Swim")
    {
      isSwiming = true;
    }
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
      t.parent = null;
    if (other.gameObject.layer == 11)
    {
      stairZone = false;
      inStair = false;
    }

    if (other.gameObject.name == "Swim")
    {
      isSwiming = false;
    }

    if (other.gameObject.name == "GazZona")
    {
      inGazZone = false;
    }
  }

  private IEnumerator OffMoveBoxAnim(float time)
  {
    yield return new WaitForSeconds(time);
    if (!moveBox)
    {
      moveBoxAnim = false;
      //characterController.radius = 0.3f;
      //characterController.radius = 0.391f;
    }
  }
  //==================================================================================================================
  public void Jump()
  {
    if ((characterController.isGrounded || visotaDown < 0.31f) && !jump && !dead && !polzet && !inStair && !isSwiming)
    {
      var handler = CharacterJump;
      if (handler != null)
        handler();
      velocity = -jumpHeight;
      jump = true;
      animation[jumpClip.name].time = 0;
      SetAnimOnce(jumpClip, 0.3f);
    }
    //Заскок на лестницу (раб)
    //if (stairZone)
    //  inStair = !inStair;
  }
  //
  //==================================================================================================================
  public void JumpToStair(bool right)
  {
    if (!jump && !dead)
    {
      if (right)
        jumpToStair = 1;
      else 
        jumpToStair = 2;
    }
  }
  //==================================================================================================================
  public void EndJump()
  {
    jump = false;
    progressBar.joysticValue.y = 0;
  }
  //==================================================================================================================
  public void Attack()
  {
    if (!kulak && Patrons[currentArmo] > 0 && !dead && !shooting && !polzet && !inStair && !isSwiming)
    {
      kulak = true;
      Shoot();
    }
  }
  //=================================================================================================================
  private IEnumerator RestartArmo(float time)
  {
    yield return new WaitForSeconds(time);
    if (kulak)
    {
      Shoot();
    }
    else
    {
      shooting = false;
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
    foreach (var a in armosGUI)
    {
      if (!all)
        foreach (var aObjs in a.ActiveObjs)
        {
          aObjs.SetActive(true);
        }

      foreach (var daObjs in a.DeactiveObjs)
      {
        daObjs.SetActive(false);
      }

      if (a.State == 2)
        a.State = 1;
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
      if (isSwiming)
        SetAnimOnce(swimDeadClip, 0.5f);
      else
      {
        if (UnityEngine.Random.value > 0.5f)
          SetAnimOnce(deadClip, 0.5f);
        else 
          SetAnimOnce(deadBackClip, 0.5f);
      }
      dead = true;
      //deadSprite.enabled = true;
    }
  }
  //==================================================================================================================
  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawRay(transform.position + Vector3.up * 0.3f, -Vector3.up * visotaDown);
    Gizmos.color = Color.green;
    Gizmos.DrawRay(transform.position + Vector3.up*0.3f, Vector3.up*visotaUp);
    Gizmos.color = Color.white;
    Gizmos.DrawRay(hand.position, -hand.forward*visotaShoot);
  }
  //==================================================================================================================
  private void Shoot()
  {
    Patrons[currentArmo] -= 1;
    if (Patrons[currentArmo] < 1 || currentArmo == 0)
      armosGUI[currentArmo].Counter.text = "";
    else
      armosGUI[currentArmo].Counter.text = Patrons[currentArmo].ToString("f0");


    //Анимация атаки на бегу
    if (Mathf.Abs(progressBar.joysticValue.x) > 30)
    {
      SetAnimOnce(armo[currentArmo].AttackRun, 1);
      StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length / armo[currentArmo].AnimSpeed));
      shooting = true;
    }
    else
    {
      //Обычная атака
      if (nearMonstr == 0)
      {
        SetAnimOnce(armo[currentArmo].ArmoAnim, armo[currentArmo].AnimSpeed);
        StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length / armo[currentArmo].AnimSpeed));
        shooting = true;
      }
      else //стреляем вниз
      {
        SetAnimOnce(armo[currentArmo].ArmoDownAnim, armo[currentArmo].AnimSpeed);
        StartCoroutine(RestartArmo(armo[currentArmo].ArmoDownAnim.length / armo[currentArmo].AnimSpeed));
        shooting = true;
      }
    }

    StartCoroutine(ShootParticleOn(0.01f));
    audio.clip = armo[currentArmo].ArmoClip;
    if (controller != null)
      audio.volume = controller.EffectsVolume;
    audio.Play();

    var handler = CharacterAttack;
    if (handler != null)
      handler(currentArmo);

    while (Patrons[currentArmo] < 1)
    {
      armosGUI[currentArmo].State = 0;
      ResetArmo(true);
      armo[currentArmo].ShootParticle.emit = false;
      CurrentArmo -= 1;
      armo[currentArmo].ShootParticle.emit = true;
      if (armosGUI[currentArmo].State == 1)
      {
        armosGUI[currentArmo].State = 2;

        foreach (var a in armosGUI[currentArmo].ActiveObjs)
        {
          a.SetActive/*Recursively*/(true);
        }
      }
    }
  }

  //------------------------------------------------------------------------
  private IEnumerator ShootParticleOn(float time)
  {
    yield return new WaitForSeconds(time);
    if (currentArmo == 1)//Pistol
      Instantiate(PistolPartPrefab, endPistol.position, endPistol.rotation);
    else
      armo[currentArmo].ShootParticle.emit = true;
    if (!kulak)
      StartCoroutine(ShootParticleOff(0.01f));

    //ЛУЧ ВЫСТРЕЛА
    if (currentArmo == 4)
    {
      RaycastHit[] hits;
      hits = Physics.RaycastAll(hand.position, -hand.forward, 3);
      int i = 0;
      visotaShoot = 100;
      while (i < hits.Length)
      {
        RaycastHit hit = hits[i];
        int collLayer = hit.collider.gameObject.layer;
        if (collLayer == 0)//Не лифт lesn
          visotaShoot = Mathf.Min(hit.distance, visotaShoot);
        i++;
      }
      if (GameObject.Find("BlastRPG(Clone)") == null)
        Instantiate(BlastPrefab, hand.position - hand.forward * (visotaShoot-0.05f), Quaternion.identity);
    }
  }
  //выключение частиц после EndAttack
  private IEnumerator ShootParticleOff(float time)
  {
    yield return new WaitForSeconds(time);
    armo[currentArmo].ShootParticle.emit = false;
  }

  private IEnumerator DeadSpriteOn(float time)
  {
    yield return new WaitForSeconds(time);
    enableDead = true;
  }
}
