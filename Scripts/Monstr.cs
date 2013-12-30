using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Monstr : MonoBehaviour
{
  [SerializeField] private Character character = null;
  [SerializeField] private BecameInvisible becameInVisible = null;
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip idleClip = null;
  [SerializeField] private AnimationClip runClip = null;
  [SerializeField] private AnimationClip attackClip = null;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AudioClip attackSound = null;
  [SerializeField] private AudioClip deadSound = null;
  [SerializeField] private float uron = 5;
  [SerializeField] private float[] uronDist = new float[5];
  [SerializeField] private float[] uronMonstr = new float[5];
  [SerializeField] private float runDist = 1;
  [SerializeField] private float attackDist = 0.2f;
  [SerializeField] private float speed = 0.6f;
  [SerializeField] private float height = 0;//Rat - 0, Bat - 0.3f
  [SerializeField] private float minX = 0;
  [SerializeField] private float maxX = 0;
  [SerializeField] private float rotSpeed = 300;
  private Transform t = null;
  private Transform characterT = null;
  private bool run = false;
  private bool att = false;
  private bool dead = false;
  private float helth = 100;
  private float distToChar = 10;
  private bool moveDown = false;

	
	private void Attack()
	{
    if (!dead && character.Helth>1)
    {
      audio.clip = attackSound;
	    audio.Play();
	    character.Helth -= uron;
      SetAnim(attackClip, 1);
	    att = true;
      StartCoroutine(EndAttack(attackClip.length));
    }
	}

  private void Start()
  {
    SetAnim(idleClip, 1);
    t = transform;
    characterT = character.transform;
    character.CharacterAttack += CharacterAttack;
    becameInVisible.ExtiRender += ExtiRender;
  }

  private void OnDestroy()
  {
    character.CharacterAttack -= CharacterAttack;
    becameInVisible.ExtiRender -= ExtiRender;
  }

  private void Update()
  {
    float heigToChar = Mathf.Abs(t.position.y - characterT.position.y - height);//разница по высоте с персонажем
    if (heigToChar < 0.2f && characterT.position.x > minX && characterT.position.x < maxX && !dead)
    {
      //ПОВОРОТЫ
      if (characterT.position.x > t.position.x)
      {
        if (t.eulerAngles.y > 90)
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(90, t.eulerAngles.y - rotSpeed*Time.deltaTime), t.eulerAngles.z);
        else
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(90, t.eulerAngles.y + rotSpeed*Time.deltaTime), t.eulerAngles.z);
      }
      else
      {
        if (t.eulerAngles.y < 270 && t.eulerAngles.y > 70)
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(270, t.eulerAngles.y + rotSpeed*Time.deltaTime), t.eulerAngles.z);
        else
          t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(270, t.eulerAngles.y - rotSpeed*Time.deltaTime), t.eulerAngles.z);
      }

      if (!att && !run)
      {
        if (Mathf.Abs(180 - t.eulerAngles.y) - 90 < -2)
          SetAnim(runClip, 1); //поворачивается
        else
          SetAnim(idleClip, 1);
      }

      //ДВИЖЕНИЕ
      distToChar = Vector3.Distance(t.position, characterT.position);

      if (distToChar > runDist)
      {
        run = false;
      }

      if (distToChar < runDist && distToChar > attackDist)
      {
        run = true;

        if (t.position.x <= maxX && t.position.x >= minX)
          t.Translate(Vector3.forward * Time.deltaTime * speed);

        if (t.position.x > maxX)
        {
          t.position = new Vector3(maxX, t.position.y, 0);
          run = false;
        }
        if (t.position.x < minX)
        {
          t.position = new Vector3(minX, t.position.y, 0);
          run = false;
        }
      }

      if (distToChar < attackDist && !att)
      {
        run = false;
        att = true;
        Attack();
      }

      t.position = new Vector3(t.position.x, t.position.y, 0);
    }
    else
    {
      run = false;
      distToChar = 10000;
    }
    if (!att && !dead)
      SetAnim(run ? runClip : idleClip, 1);

    if (moveDown)
      t.position -= Vector3.up * height * Time.deltaTime;
  }
  //--------------------------------------------------------------------------------------------------
  private void CharacterAttack(int armo)
  {
    if (distToChar < uronDist[armo] && !dead && Mathf.Abs(character.transform.eulerAngles.y - t.eulerAngles.y)>170)
    {
      helth -= uronMonstr[armo];
      SetAnim(deadClip, 0.5f);
      if (helth < 0)
      {
        dead = true;
        audio.clip = deadSound;
        audio.Play();
        StopAllCoroutines();
        if (height > 0)//Падение после смерти
        {
          StartCoroutine(EndDown(1));
          moveDown = true;
        }
      }
    }
  }

  private IEnumerator EndDown(float time)
  {
    yield return new WaitForSeconds(time);
    moveDown = false;
  }

  //--------------------------------------------------------------------------------------------------
  private IEnumerator EndAttack(float time)
  {
    yield return new WaitForSeconds(time);
    att = false;
  }

  private void SetAnim(AnimationClip cl, float sp)
  {
    anim.clip = cl;
    anim[cl.name].speed = sp;
    anim.Play(cl.name);
  }

  private void ExtiRender()
  {
    Debug.Log(" OnBecameInvisible");
    if (dead)
      Destroy(gameObject);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawRay(new Vector3(minX, transform.position.y, 0), Vector3.right*(maxX - minX));
    Gizmos.DrawRay(new Vector3(minX, transform.position.y+0.01f, 0), Vector3.right * (maxX - minX));
  }
}
