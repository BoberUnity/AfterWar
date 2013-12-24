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
  }

  [SerializeField] private Armo[] armo = null;
  [SerializeField] private UIProgressBar progressBar = null;
  [SerializeField] private AnimationClip jumpClip = null;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AnimationClip stairClip = null;
  [SerializeField] private AnimationClip stairIdleClip = null;
  [SerializeField] private AnimationClip actionClip = null;
  [SerializeField] private UISprite deadSprite = null;
  [SerializeField] private UISprite helthSprite = null;
  [SerializeField] private float curSpeed = 1;
  [SerializeField] private float rotSpeed = 580;
  [SerializeField] private float stairSpeed = 1;
  [SerializeField] private float gravSpeed = 2;
  [SerializeField] private float jumpHeight = 1;
  [SerializeField] private float helth = 80;
    private float visotaDown = 1;
    [SerializeField]
    private float visotaUp = 1;
    private bool liftZone;
    private float velocity = 0;
    private CharacterController characterController = null;
    private Transform t = null;
    private bool jump = false;
    private bool kulak = false;
    private bool dead = false;
    private bool act = false;

  public event Action<string> TriggerEnter;

  //[SerializeField] private float v;

  private bool stairZone = false;
  
  private int currentArmo = 0;

  public float Helth
  {
    get { return helth; }
    set 
    { 
      helth = Mathf.Clamp(value, 0, 100); 
      SetHelth();
    }
  }

  public int CurrentArmo
  {
    get { return currentArmo; }
    set { currentArmo = value; }
  }
  //==================================================================================================================
	void Start ()
	{
	  Application.targetFrameRate = 300;
	  characterController = GetComponent<CharacterController>();
	  t = transform;
	}

  //==================================================================================================================
  void Update ()
  {
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
      Debug.LogWarning("visotaUp = " + visotaUp);
    }

    //Gravity
    if (!stairZone)
    {
      if (characterController.isGrounded && !jump)
      {
        velocity = 0;
      }
      else
      {
        characterController.Move(-Vector3.up * Time.deltaTime * velocity);
        velocity += Time.deltaTime * gravSpeed;
      }
    }
    //Moving
    if (Mathf.Abs(t.localPosition.z) > 0.02)
      t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);

    if (!dead && !stairZone && !act)
    {
      if (Mathf.Abs(progressBar.joysticValue.x) > 20)
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
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y - rotSpeed*Time.deltaTime, t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y + rotSpeed * Time.deltaTime, t.eulerAngles.z);
        }

        if (progressBar.joysticValue.x < 0)
        {
          if (t.eulerAngles.y < 270 && t.eulerAngles.y > 70)
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y + rotSpeed*Time.deltaTime, t.eulerAngles.z);
          else
            t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y - rotSpeed * Time.deltaTime, t.eulerAngles.z);
        }
      }
      else
      {
        if (!kulak)
          SetAnimCross(armo[currentArmo].ArmoIdle, 1);
      }
    }
    if (stairZone)
    {
      if (Mathf.Abs(progressBar.joysticValue.y) > 20)
      {
        float step = progressBar.joysticValue.y * 0.01f * stairSpeed;
        characterController.Move(Vector3.up * Time.deltaTime * step);
        SetAnimCross(stairClip, Mathf.Abs(step));
      }
      else
      {
        SetAnimCross(stairIdleClip, 0.1f);
      }
      t.localRotation = Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y / (1 + rotSpeed) * Time.deltaTime * 0.01f, t.eulerAngles.z);
    }
    //lift
    if (liftZone && !jump)
    {
      if (visotaDown < 0.28f)
        t.localPosition += Vector3.up * (0.28f - visotaDown);
      if (visotaDown > 0.32f && visotaDown < 0.35f)
        t.localPosition += Vector3.up * (0.32f - visotaDown);
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
  private IEnumerator EndJump (float time)
    {
        yield return new WaitForSeconds(time);
        jump = false;
        progressBar.joysticValue.y = 0;
    }
  //==================================================================================================================
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 10)
      liftZone = true;
    if (other.gameObject.layer == 11)
      stairZone = true;

    if (other.gameObject.layer == 12)//Action
    {
      act = true;
      SetAnimOnce(actionClip, 0.5f);
      StartCoroutine(ActOff(actionClip.length/*, other*/));
    }

    if (other.gameObject.name == "Rat")
    {
      Helth -= 10;
      Monstr m = other.GetComponent<Monstr>();
      if (m != null)
        m.Attack();
      else
        Debug.LogWarning("Monstr has no component");
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
      liftZone = false;
    if (other.gameObject.layer == 11)
      stairZone = false;
  }
  //==================================================================================================================
  public void Jump()
  {
    if (characterController.isGrounded && !jump)
    {
      velocity = -jumpHeight;
      jump = true;
      SetAnimOnce(jumpClip, 0.4f);
      StartCoroutine(EndJump(jumpClip.length));
    }
    stairZone = false;
  }
  //==================================================================================================================
  public void Attack()
  {
    if (!kulak)
    {
      kulak = true;
      SetAnimOnce(armo[currentArmo].ArmoAnim, 0.7f);
      armo[currentArmo].ShootParticle.emit = true;
      audio.clip = armo[currentArmo].ArmoClip;
      audio.Play();
      StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length));
    }
  }
  //=================================================================================================================
  private IEnumerator RestartArmo(float time)
  {
    yield return new WaitForSeconds(time);
    if (kulak)
    {
      SetAnimOnce(armo[currentArmo].ArmoAnim, 0.7f);
      audio.clip = armo[currentArmo].ArmoClip;
      audio.Play();
      StartCoroutine(RestartArmo(armo[currentArmo].ArmoAnim.length));
    }
  }
  //==================================================================================================================
  public void EndAttack()
  {
    kulak = false;
    armo[currentArmo].ShootParticle.emit = false;
  }
  //==================================================================================================================
  public void ResetArmo()
  {
    foreach (var a in armo)
    {
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
  //==================================================================================================================

  private IEnumerator ActOff(float time/*, Collider other*/)
  {
    yield return new WaitForSeconds(time);
    act = false;
    //var handler = TriggerEnter;
    //if (handler != null)
    //  handler(other.gameObject.name);
  }
  //==================================================================================================================
  private void SetHelth()
  {
    helthSprite.width = (int)helth;
    helthSprite.transform.localPosition = new Vector3(-50 + helth/2, 0, 0);
    if (helth < 1)
    {  
      SetAnimOnce(deadClip, 0.5f);
      dead = true;
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
