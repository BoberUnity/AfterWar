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
  [SerializeField] private GameObject gitara = null;
  [SerializeField] private float uron = 5;
  [SerializeField] private float[] uronDist = new float[5];
  [SerializeField] private float[] uronMonstr = new float[5];
  [SerializeField] private bool addRpgForce = false;
  //[SerializeField] private Vector3 boxColliderCenter = Vector3.zero;
  //[SerializeField] private Vector3 boxColliderSize = Vector3.one * 0.4f;
  [SerializeField] private float rpgForceX = 250;
  [SerializeField] private float rpgForceY = 50;
  [SerializeField] private float runDist = 1;
  [SerializeField] private float attackDist = 0.2f;
  [SerializeField] private float speed = 0.6f;
  //[SerializeField] private float height = 0;//Rat - 0, Bat - 0.3f
  private float minX = 0;
  private float maxX = 0;
  [SerializeField] private float leftZona = 1;
  [SerializeField] private float rightZona = 1;
  [SerializeField] private float rotSpeed = 300;
  [SerializeField] private float nearDist = 0.6f;//attack down
  //[SerializeField] private bool winEnabled = false;
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
  //private bool moveDown = false;
  private bool win = false;
  [SerializeField] private float currWeight = 1.0f;
  [SerializeField] private AnimationClip oldClip = null;
  //[SerializeField] private float oldWeight = 0.0f;
	
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
    oldClip = anim.clip;
    anim[oldClip.name].enabled = true;
    if (gitara != null)
      SetAnim(idleClip, 1);
    else
      SetAnim(upClip, 0);
    
    t = transform;
    zPos = t.position.z;
    characterT = character.transform;
    character.CharacterAttack += CharacterAttack;
    becameInVisible.ExtiRender += ExtiRender;
    minX = t.position.x - leftZona;
    maxX = t.position.x + rightZona;
  }

  private void OnDestroy()
  {
    character.CharacterAttack -= CharacterAttack;
    becameInVisible.ExtiRender -= ExtiRender;
  }

  private void Update()
  {
    //animation[runClip.name].weight = runWeight;
    //animation[idleClip.name].weight = 1 - runWeight;

    float heigToChar = Mathf.Abs(t.position.y - characterT.position.y);//разница по высоте с персонажем

    if (heigToChar < 0.3f && characterT.position.x > minX && characterT.position.x < maxX && !dead && isActive == 0)
    {
      distToChar = Vector3.Distance(t.position, characterT.position);
      if (distToChar < runDist)
      {
        isActive = 1;
        StartCoroutine(EndUpAnim(upClip.length*0.33f));
        StartCoroutine(ReleaseGitara(0.3f));
        //SetAnim(upClip,3);
        //
        //anim[anim.clip.name].enabled = false;
        if (gitara != null)
        {
          oldClip = anim.clip;//??
          currWeight = 0;
        }
        anim.clip = upClip;
        anim[upClip.name].speed = 3;
        anim[upClip.name].enabled = true;
      }
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

      if (!att && !run)
      {
        if (Mathf.Abs(180 - t.eulerAngles.y) - 90 < -2)
          SetAnim(runClip, 1); //поворачивается
        else
          SetAnim(idleClip, 1);
      }

      //ДВИЖЕНИЕ
      RaycastHit[] hits;
      hits = Physics.RaycastAll(t.position + Vector3.up * 0.2f, Vector3.right * Mathf.Sign(characterT.position.x - t.position.x), 5);
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
      if (win)//Уходит назад после победы
        t.Translate(Vector3.forward * Time.deltaTime * speed);
      
      if (distToChar < nearDist && !isNear && !dead)
      {
        isNear = true;
        character.NearMonstr += 1;
      }

      if (distToChar > nearDist && isNear)
      {
        isNear = false;
        character.NearMonstr -= 1;
      }

      if (distToChar < attackDist && !att)
      {
        run = false;
        att = true;
        Attack();
      }

      //if (distToChar < attackDist-0.05f && !winEnabled)//для людей отключим движение назад/отталкивание
      //  t.Translate(-Vector3.forward * Time.deltaTime * speed);

      t.position = new Vector3(t.position.x, t.position.y, zPos);
    }
    else
    {
      run = false;
      distToChar = 10000;
    }

    if (!att && !dead && isActive == 2)
      SetAnim(run ? runClip : attackClip, run ? 1:0.1f);
    //Сглаживание анимаций
    if (currWeight < 1)
    {
      currWeight += Time.deltaTime * 4.0f;
      if (currWeight > 1)
      {
        currWeight = 1;
        anim[oldClip.name].enabled = false;
      }
      anim[anim.clip.name].weight = currWeight;
      anim[oldClip.name].weight = 1 - currWeight;
    }

    //if (moveDown)
    //  t.position -= Vector3.up * height * Time.deltaTime;
  }
  //--------------------------------------------------------------------------------------------------
  private void CharacterAttack(int armo)
  {
    float heigToChar = Mathf.Abs(t.position.y - characterT.position.y);//разница по высоте с персонажем
    distToChar = Vector3.Distance(t.position, characterT.position);
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
      
      if (helth < 0)
      {
        if (Random.value > 0.5f)
          SetAnim(deadClip, 1);
        else
          SetAnim(dead2Clip, 1);
        
        if (addRpgForce)
        {
          if (armo == 2 || armo == 4)
          {
            //BoxCollider boxCollider = gameObject.AddComponent("BoxCollider") as BoxCollider;
            //if (boxCollider != null)
            //{
            //  boxCollider.center = boxColliderCenter;//new Vector3(0, 0.15f, -0.8f);
            //  boxCollider.size = boxColliderSize;//new Vector3(0.3f, 0.3f, 1.8f);
            //}
            gameObject.AddComponent("Rigidbody");
            if (rigidbody != null)
            {
              rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
              rigidbody.freezeRotation = true;
              if (characterT.position.x < t.position.x)
                rigidbody.AddForce(rpgForceX, rpgForceY, 0);
              else
                rigidbody.AddForce(-rpgForceX, rpgForceY, 0);
            }
            SetAnim(deadRPGClip, 1);
          }
        }
        
        dead = true;
        if (character.Controller != null)
          audio.volume = character.Controller.EffectsVolume;
        audio.clip = deadSound;
        audio.Play();
        if (isNear)
        { 
          character.NearMonstr -= 1;
          isNear = false;
        }
        
        //Destroy(GetComponent<BoxCollider>(), 0.2f);
        if (armo == 4 && GameObject.Find("BlastRPG(Clone)") == null)//Лишняя проверка
          Instantiate(blastPrefab, t.position + Vector3.up*0.25f, t.rotation);
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

  private IEnumerator ReleaseGitara(float time)
  {
    yield return new WaitForSeconds(time);
    if (gitara != null) //Бросить гитару
    {
      gitara.transform.parent = null;
      gitara.AddComponent("Rigidbody");
      if (characterT.position.x < t.position.x)
        gitara.rigidbody.AddForce(-70, 0, 0);
      else
        gitara.rigidbody.AddForce(70, 0, 0);

      fire.transform.parent.gameObject.SetActive(true);
    }
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
    if (anim.clip != cl)
    {
      if (currWeight < 1)
        anim[oldClip.name].enabled = false;
      oldClip = anim.clip;
      currWeight = 0;
      if (oldClip.name == "gitara" && isActive == 2)
      {
        currWeight = 0.99f;
      }
      anim.clip = cl;
      anim[cl.name].speed = sp;
      anim[anim.clip.name].enabled = true;
    }
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
    Gizmos.color = editorColor;
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y, 0), Vector3.right * (rightZona + leftZona));
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y + 0.1f, 0), Vector3.right * (rightZona + leftZona));
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y, 0), Vector3.up * 0.1f);
    Gizmos.DrawRay(new Vector3(transform.position.x + rightZona, transform.position.y, 0), Vector3.up * 0.1f);
    //Gizmos.color = Color.yellow;
    //Gizmos.DrawRay(transform.position + Vector3.right * 1.5f, -Vector3.up * 0.5f);
  }
}
