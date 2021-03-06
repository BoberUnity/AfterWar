﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Monstr : MonoBehaviour
{
  [SerializeField] private Color editorColor = Color.red;
  /*[SerializeField]*/ private Character character = null;
  [SerializeField] private BecameInvisible becameInVisible = null;
  [SerializeField] private Animation anim = null;
  [SerializeField] private AnimationClip idleClip = null;
  [SerializeField] private float idleSpeed = 1;
  [SerializeField] private AnimationClip runClip = null;
  [SerializeField] private float runSpeed = 1;
  [SerializeField] private AnimationClip attackClip = null;
  [SerializeField] private float attackAnimSpeed = 1;
  [SerializeField] private AnimationClip eatClip = null;
  [SerializeField] private bool eatEnabled = false;
  [SerializeField] private AnimationClip deadClip = null;
  [SerializeField] private AnimationClip dead2Clip = null;
  [SerializeField] private AnimationClip deadRPGClip = null;
  [SerializeField] private AudioClip attackSound = null;
  [SerializeField] private AudioClip charAttackSound = null;
  [SerializeField] private AudioClip deadSound = null;
  [SerializeField] private GameObject blastPrefab = null;
  [SerializeField] private ParticleEmitter fire = null;
  [SerializeField] private Transform[] rigBoxes = null;
  [SerializeField] private float uron = 5;
  [SerializeField] private float[] uronDist = new float[5];
  [SerializeField] private float[] uronMonstr = new float[5];
  //[SerializeField] private Vector3 boxColliderCenter = Vector3.zero;
  //[SerializeField] private Vector3 boxColliderSize = Vector3.one*0.4f;
  [SerializeField] private Vector2[] armoForce = new Vector2[5];
  //[SerializeField] private float rpgForceX = 350;
  //[SerializeField] private float rpgForceY = 50;
  [SerializeField] private float runDist = 1;
  [SerializeField] private float jumpDist = 1;
  [SerializeField] private float attackDist = 0.2f;
  [SerializeField] private float speed = 0.6f;
  [SerializeField] private float height = 0;//Rat - 0, Bat - 0.3f
  [SerializeField] private float paukHeight = 0;
  [SerializeField] private Transform pautinaT = null;
  [SerializeField] private Transform bodyT = null;
  [SerializeField] private float blastHeight = 0;
  [SerializeField] private float leftZona = 1;
  [SerializeField] private float rightZona = 1;
  [SerializeField] private float rotSpeed = 300;
  [SerializeField] private float nearDist = 0.6f;//attack down
  [SerializeField] private bool winEnabled = false;
  [SerializeField] private bool railway = false;
  [SerializeField] private Vector3 jumpForce = new Vector3(250,100,0);
  private float minX = 0;
  private float maxX = 0;
  private float zPos = 0;
  private bool isNear = false;
  private Transform t = null;
  private Transform characterT = null;
  private BoxCollider trigger = null;
  private bool run = false;
  private bool att = false;
  private bool dead = false;
  private float helth = 100;
  private float distToChar = 10;
  private bool moveDown = false;
  private bool win = false;
  private float currWeight = 1.0f;
  private AnimationClip oldClip = null;
  private bool follow = false;
  private bool jump = false;
  //private Vector3 pautinaScale = Vector3.zero;
	
	
	private void Attack()
	{
    if (!dead && character.Helth>1)
    {
      GetComponent<AudioSource>().clip = attackSound;
      if (character.Controller != null)
        GetComponent<AudioSource>().volume = character.Controller.EffectsVolume;
      else Debug.LogWarning("character.Controller != null");
	    GetComponent<AudioSource>().Play();
	    character.BronHelth -= uron;
      SetAnim(attackClip, attackAnimSpeed);
	    att = true;
      StartCoroutine(EndAttack(attackClip.length / attackAnimSpeed));
      if (fire)
        fire.emit = true;
    }
	}

  private void Jump()
  {
    if (gameObject.GetComponent<Rigidbody>() == null)
    {
      gameObject.AddComponent<Rigidbody>();
      GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
    }
    GetComponent<Rigidbody>().AddForce(jumpForce.x, jumpForce.y, jumpForce.z);
    jump = true;
    //StartCoroutine(JumpEnable(0.7f));
    Debug.LogWarning("Rig "+Time.time);
  }

  private IEnumerator JumpEnable(float time)
  {
    yield return new WaitForSeconds(time);
    jump = false;
  }

  private void Start()
  {
    character = GameObject.Find("Stalker").GetComponent<Character>();
    oldClip = anim.clip;
    anim[oldClip.name].enabled = true;
    SetAnim(idleClip, idleSpeed);
    t = transform;
    zPos = t.position.z;
    characterT = character.transform;
    character.CharacterAttack += CharacterAttack;
    becameInVisible.ExtiRender += ExtiRender;
    minX = t.position.x - leftZona;
    maxX = t.position.x + rightZona;
    trigger = GetComponent<BoxCollider>();
    //if (pautinaT != null)
    //{
    //  pautinaScale = pautinaT.localScale;
    //}
  }

  private void OnDestroy()
  {
    character.CharacterAttack -= CharacterAttack;
    becameInVisible.ExtiRender -= ExtiRender;
  }

  private void Update()
  {
    float heigToChar = Mathf.Abs(t.position.y - characterT.position.y - height);//разница по высоте с персонажем
    if (railway && !follow)
    {
      heigToChar = 1;
      if (characterT.position.x > t.position.x + 1.7f)
      {
        heigToChar = 0;
        follow = true;
      }
    }

    if (heigToChar < 0.2f && characterT.position.x > minX && characterT.position.x < maxX && !dead && paukHeight < 0.01f)
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
      hits = Physics.RaycastAll(t.position + Vector3.up * Mathf.Min(0.2f,height), Vector3.right * Mathf.Sign(characterT.position.x - t.position.x), 5);
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

      distToChar = Mathf.Abs(t.position.x - characterT.position.x);//Vector3.Distance(t.position, characterT.position);

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

      if (railway)
      {
        if (characterT.parent != null)
        {
          float distToVagonetka = Vector3.Distance(t.position, characterT.parent.position);
          if (distToVagonetka < jumpDist && !jump)
            Jump();
        }
      }

      if (distToChar < attackDist && !att && paukHeight < 0.01f)
      {
        run = false;
        att = true;
        Attack();
      }

      if (distToChar < attackDist-0.05f && !winEnabled)//для людей отключим движение назад
        t.Translate(-Vector3.forward * Time.deltaTime * speed);

      if (railway) 
        t.position = new Vector3(t.position.x, t.position.y, 0);
      else
        t.position = new Vector3(t.position.x, t.position.y, zPos);
    }
    else
    {
      run = false;
      distToChar = 10000;
    }
    //only pauk
    if ((paukHeight > 0 || paukHeight < 0) && heigToChar < 0.37f  && (characterT.position.x > minX && characterT.position.x < maxX || dead))
    {
      run = true;
      if (paukHeight > 0)
      {
        t.position -= Vector3.up * 0.3f * Time.deltaTime;
        paukHeight -= Time.deltaTime * 0.3f;
        pautinaT.localScale += Vector3.forward * Time.deltaTime * 0.6f;
        
        if (paukHeight < 0.05f)
        {
          bodyT.eulerAngles += Vector3.forward * Time.deltaTime * 540;
          pautinaT.GetComponent<Renderer>().material.color -= new Color(0, 0, 0, Time.deltaTime*6);
        }
      }
      if (paukHeight < 0)
        bodyT.eulerAngles = new Vector3(bodyT.eulerAngles.x, bodyT.eulerAngles.y, 0);
    }
    //

    if (win && eatEnabled)
    {
      SetAnim(eatClip, 1);
    }
    else
    {
      if (!att && !dead)
      {
        if (run)
          SetAnim(runClip, runSpeed);
        else 
          SetAnim(idleClip, idleSpeed);
        //SetAnim(run ? runClip : idleClip, 1);
      }
    }

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

    if (moveDown)
    {
      t.position -= Vector3.up * height * Time.deltaTime;
    }

    if (dead || moveDown)//Удаление при столкновении с ящиком
    {
      foreach (var box in rigBoxes)
      {
        if (Vector3.Distance(t.position, box.position) < 0.16f)
          Destroy(gameObject);
      }
    }
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
    
    var charPovernut = characterT.eulerAngles.y > 20 && characterT.eulerAngles.y < 160 && /*Mathf.Abs*/(characterT.position.x - t.position.x) < 0.2f;//ГГ повернут вправо и монстр справа
    
    if (characterT.eulerAngles.y > 200 && characterT.eulerAngles.y < 340 && characterT.position.x - t.position.x > -0.2f)//ГГ повернут влево и монстр слева
      charPovernut = true;


    if (distToChar < uronDist[armo] && !dead && charPovernut && heigToChar < 0.35f && notWall && Mathf.Abs(t.position.z) < 0.6f)
    {
      helth -= uronMonstr[armo];
      if (helth < 0)
      {
        if (armoForce[armo].x > 0/*armo == 2 || armo == 4*/)
        {
          //BoxCollider boxCollider = gameObject.AddComponent("BoxCollider") as BoxCollider;
          //if (boxCollider != null)
          //{
          //  boxCollider.center = boxColliderCenter;//new Vector3(0, 0.15f, -0.8f);
          //  boxCollider.size = boxColliderSize;//new Vector3(0.3f, 0.3f, 1.8f);
          //}
          if (GetComponent<Rigidbody>() == null)
          gameObject.AddComponent<Rigidbody>();
          if (GetComponent<Rigidbody>() != null)
          {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            GetComponent<Rigidbody>().freezeRotation = true;
            //rigidbody.drag = 5;

            if (characterT.position.x < t.position.x)
              GetComponent<Rigidbody>().AddForce(armoForce[armo].x, armoForce[armo].y, 0);
            else
              GetComponent<Rigidbody>().AddForce(-armoForce[armo].x, armoForce[armo].y, 0);
          }
          SetAnim(deadRPGClip, 1);
        }
        else
        {
          if (Random.value > 0.5f)
            SetAnim(deadClip, 1);
          else
            SetAnim(dead2Clip, 1);
        }

        dead = true;
        if (character.Controller != null)
          GetComponent<AudioSource>().volume = character.Controller.EffectsVolume;
        GetComponent<AudioSource>().clip = deadSound;
        GetComponent<AudioSource>().Play();
        if (isNear && height < 0.1f)
        { 
          character.NearMonstr -= 1;
          isNear = false;
        }
        StopAllCoroutines();
        StartCoroutine(DestroyCollider(2));
        Destroy(trigger, 0.2f);//????
        if (armo == 4 && GameObject.Find("BlastRPG(Clone)") == null)//лишняя проверка
        {
          /*GameObject blastObj = */
          Instantiate(blastPrefab, t.position + Vector3.up * blastHeight, t.rotation)/* as GameObject*/;
          //if (blastObj)
          //  blastObj.transform.parent = t;
        }
        if (fire)
          fire.emit = false;

        if (height > 0)
        {
          moveDown = true;
          StartCoroutine(EndDown(1));
        }
      }
      else
      {
        if (character.Controller != null)
          GetComponent<AudioSource>().volume = character.Controller.EffectsVolume;
        else Debug.LogWarning("character.Controller != null");
        GetComponent<AudioSource>().clip = charAttackSound;
        GetComponent<AudioSource>().Play();
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
  }

  private IEnumerator DestroyCollider(float time)
  {
    yield return new WaitForSeconds(time);
    BoxCollider coll = GetComponent<BoxCollider>();
    if (coll != null)
    {
      if (GetComponent<Rigidbody>() != null)
        GetComponent<Rigidbody>().useGravity = false;
      Destroy(coll);
    }
  }

  private void SetAnim(AnimationClip cl, float sp)
  {
    if (currWeight < 1)
      anim[oldClip.name].enabled = false;
    
    if (anim.clip != cl)
    {
      oldClip = anim.clip;
      currWeight = 0;
    }
    else
    {
      currWeight = 0.99f;
    }
    anim.clip = cl;
    anim[cl.name].speed = sp;
    anim[anim.clip.name].enabled = true;
  }

  private void ExtiRender()
  {
    if (dead)
      Destroy(gameObject);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = editorColor;
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y, 0), Vector3.right * (rightZona + leftZona));
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y + 0.1f, 0), Vector3.right * (rightZona + leftZona));
    Gizmos.DrawRay(new Vector3(transform.position.x - leftZona, transform.position.y, 0), Vector3.up * 0.1f);
    Gizmos.DrawRay(new Vector3(transform.position.x + rightZona, transform.position.y, 0), Vector3.up * 0.1f);
  }
}
