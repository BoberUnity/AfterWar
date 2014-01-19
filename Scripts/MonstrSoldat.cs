using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MonstrSoldat : MonoBehaviour
{
  [SerializeField] private Color editorColor = Color.red;
  [SerializeField] private Character character = null;
  [SerializeField] private BecameInvisible becameInVisible = null;
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip upClip = null;
  [SerializeField] private AnimationClip idleClip = null;
  [SerializeField] private AnimationClip runClip = null;
  [SerializeField] private AnimationClip attackClip = null;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AnimationClip dead2Clip = null;
  [SerializeField] private AnimationClip deadRPGClip = null;
  [SerializeField] private AudioClip attackSound = null;
  [SerializeField] private AudioClip charAttackSound = null;
  [SerializeField] private AudioClip deadSound = null;
  [SerializeField] private GameObject blastPrefab = null;
  [SerializeField] private ParticleEmitter fire = null;
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
  [SerializeField] private float nearDist = 0.6f;//attack down
  [SerializeField] private bool winEnabled = false;
  private int isActive = 0;//1-vstaet
  private float zPos = 0;
  private bool isNear = false;
  private Transform t = null;
  private Transform characterT = null;
  private bool run = false;
  private bool att = false;
  private bool dead = false;
  private float helth = 100;
  private float distToChar = 10;
  private bool moveDown = false;
  private bool win = false;
  //private float distToWall;
	
	private void Attack()
	{
    if (!dead && character.Helth>1)
    {
      audio.clip = attackSound;
      if (character.Controller != null)
        audio.volume = character.Controller.EffectsVolume;
      else Debug.LogWarning("character.Controller != null");
	    audio.Play();
	    character.Helth -= uron;
      //Debug.Log("Uron" + gameObject.name);
      SetAnim(attackClip, 1);
	    att = true;
      StartCoroutine(EndAttack(attackClip.length));
      if (fire)
        fire.emit = true;
    }
	}

  private void Start()
  {
    SetAnim(upClip, 0);
    t = transform;
    zPos = t.position.z;
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

    if (heigToChar < 0.3f && characterT.position.x > minX && characterT.position.x < maxX && !dead && isActive == 0)
    {
      isActive = 1;
      StartCoroutine(EndUpAnim(upClip.length*0.5f));
      SetAnim(upClip,2);
    }

    if (heigToChar < 0.3f && characterT.position.x > minX && characterT.position.x < maxX && !dead && isActive == 2)
    {
      //ПОВОРОТЫ
      if (!win)
      {
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
      }
      else//Уходит после победы
      {
        if (winEnabled)
        {
          if (characterT.position.x < t.position.x)
          {
            if (t.eulerAngles.y > 90)
              t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(90, t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z);
            else
              t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(90, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
          }
          else
          {
            if (t.eulerAngles.y < 270 && t.eulerAngles.y > 70)
              t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Min(270, t.eulerAngles.y + rotSpeed * Time.deltaTime), t.eulerAngles.z);
            else
              t.localRotation = Quaternion.Euler(t.eulerAngles.x, Mathf.Max(270, t.eulerAngles.y - rotSpeed * Time.deltaTime), t.eulerAngles.z);
          }
        }
      }

      if (!att && !run)
      {
        if (Mathf.Abs(180 - t.eulerAngles.y) - 90 < -2)
          SetAnim(runClip, 1); //поворачивается
        else
          SetAnim(idleClip, 1);
      }

      //ДВИЖЕНИЕ
      RaycastHit[] hits;
      hits = Physics.RaycastAll(t.position + Vector3.up * (0.2f - height), Vector3.right * Mathf.Sign(characterT.position.x - t.position.x), 5);
      //hits = Physics.RaycastAll(t.position + Vector3.up * (0.2f - height), characterT.position - t.position, 5);
      int i = 0;
      float distToWall = 100;
      while (i < hits.Length)
      {
        RaycastHit hit = hits[i];
        int collLayer = hit.collider.gameObject.layer;
        if (collLayer == 0)//Default layer
          distToWall = Mathf.Min(hit.distance, distToWall);
        i++;
      }

      distToChar = Vector3.Distance(t.position, characterT.position);

      if (distToChar > runDist || distToWall < 0.2f)
      {
        run = false;
      }

      if (((distToChar < runDist && distToChar > attackDist && distToWall > 0.2f) || win))
      {
        run = true;

        if (!win)
        {
          if (t.position.x <= maxX && t.position.x >= minX && distToChar > attackDist)
            t.Translate(Vector3.forward*Time.deltaTime*speed);
        }

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
      if (win && winEnabled)//Уходит назад после победы
        t.Translate(Vector3.forward * Time.deltaTime * speed);
      
      if (distToChar < nearDist && !isNear && !dead)
      {
        isNear = true;
        if (height < 0.1f)
          character.NearMonstr += 1;
      }

      if (distToChar > nearDist && isNear)
      {
        isNear = false;
        if (height < 0.1f)
          character.NearMonstr -= 1;
      }

      if (distToChar < attackDist && !att)
      {
        run = false;
        att = true;
        Attack();
      }

      if (distToChar < attackDist-0.05f && !winEnabled)//для людей отключим движение назад
        t.Translate(-Vector3.forward * Time.deltaTime * speed);

      t.position = new Vector3(t.position.x, t.position.y, zPos);
    }
    else
    {
      run = false;
      distToChar = 10000;
    }

    if (!att && !dead && isActive == 2)
    //SetAnim(run ? runClip : idleClip, 1);
      SetAnim(run ? runClip : attackClip, run ? 1:0.1f);
    

    if (moveDown)
      t.position -= Vector3.up * height * Time.deltaTime;
  }
  //--------------------------------------------------------------------------------------------------
  private void CharacterAttack(int armo)
  {
    float heigToChar = Mathf.Abs(t.position.y - characterT.position.y - height);//разница по высоте с персонажем
    distToChar = Vector3.Distance(t.position, characterT.position);
    //bool notAttacked = !isNear && character.NearMonstr > 0;//Есть кто-то другой рядом
    //Между монстром и ГГ нет стены или другого монстра
    RaycastHit[] hits;
    //hits = Physics.RaycastAll(t.position + Vector3.up * (0.2f - height), Vector3.right * Mathf.Sign(characterT.position.x - t.position.x), 5);
    hits = Physics.RaycastAll(t.position + Vector3.up * 0.2f, characterT.position - t.position, 5);
    int i = 0;
    float rayToChar = 100;
    while (i < hits.Length)
    {
      RaycastHit hit = hits[i];
      int collLayer = hit.collider.gameObject.layer;
      if (collLayer == 0)//Default layer
        rayToChar = Mathf.Min(hit.distance, rayToChar);
      i++;
    }
    bool notWall = distToChar < rayToChar;
    
    var charPovernut = characterT.eulerAngles.y > 50 && characterT.eulerAngles.y < 120 && characterT.position.x - t.position.x < 0;//ГГ повернут вправо и монстр справа
    
    if (characterT.eulerAngles.y > 230 && characterT.eulerAngles.y < 310 && characterT.position.x - t.position.x > 0)//ГГ повернут влево и монстр слева
      charPovernut = true;
    //if (distToChar < attackDist && character.NearMonstr < 2)
    //  charPovernut = true;

    if (distToChar < uronDist[armo] && !dead && charPovernut && heigToChar < 0.35f && notWall)
    {
      helth -= uronMonstr[armo];
      if (armo == 4 || armo == 2)
        SetAnim(deadRPGClip, 1);
      else
      {
        if (Random.value > 0.5f)
          SetAnim(deadClip, 1);
        else
          SetAnim(dead2Clip, 1);
      }
      if (helth < 0)
      {
        dead = true;
        if (character.Controller != null)
          audio.volume = character.Controller.EffectsVolume;
        audio.clip = deadSound;
        audio.Play();
        if (isNear && height < 0.1f)
        { 
          character.NearMonstr -= 1;
          isNear = false;
        }
        StopAllCoroutines();
        if (height > 0)//Падение после смерти
        {
          StartCoroutine(EndDown(1));
          moveDown = true;
        }
        Destroy(GetComponent<BoxCollider>(), 0.2f);
        if (armo == 4)
          Instantiate(blastPrefab, t.position, t.rotation);
        if (fire)
          fire.emit = false;
      }
      else
      {
        if (character.Controller != null)
          audio.volume = character.Controller.EffectsVolume;
        else Debug.LogWarning("character.Controller != null");
        audio.clip = charAttackSound;
        audio.Play();
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
    if (fire)
      fire.emit = false;
    win = character.Helth < 1;
    //if (win)
    //  animation[winClip.name].time = 0.5f;
  }

  private void SetAnim(AnimationClip cl, float sp)
  {
    anim.clip = cl;
    anim[cl.name].speed = sp;
    anim.Play(cl.name);
  }

  private void ExtiRender()
  {
    if (dead)
      Destroy(gameObject);
  }

  private IEnumerator EndUpAnim(float time)
  {
    yield return new WaitForSeconds(time);
    isActive = 2;
  }

  void OnDrawGizmos()
  {
    //Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, Vector3.right * Mathf.Sign(character.transform.position.x - transform.position.x) * 3);
    Gizmos.color = editorColor;
    Gizmos.DrawRay(new Vector3(minX, transform.position.y, 0), Vector3.right * (maxX - minX));
    Gizmos.DrawRay(new Vector3(minX, transform.position.y + 0.1f, 0), Vector3.right * (maxX - minX));
    Gizmos.DrawRay(new Vector3(minX, transform.position.y, 0), Vector3.up * 0.1f);
    Gizmos.DrawRay(new Vector3(maxX, transform.position.y, 0), Vector3.up * 0.1f);
    //Gizmos.DrawRay(transform.position + Vector3.up * 0.2f, /*Vector3.right * Mathf.Sign(character.transform.position.x - transform.position.x)*/(character.transform.position - transform.position) * 5);
    
 
  }
}
