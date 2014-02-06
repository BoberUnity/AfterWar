using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Truba : MonoBehaviour 
{
  [SerializeField] private GameObject blastPrefab = null;
  [SerializeField] private float delHeight = 0.4f;
  [SerializeField] private float delDist = 2.5f;
  [SerializeField] private Vector3 blastPos = Vector3.zero;
  private Character character = null;
  
  private void Start()
  {
    character = GameObject.Find("Stalker").GetComponent<Character>();
    character.CharacterAttack += CharacterAttack;
  }

  private void OnDestroy()
  {
    character.CharacterAttack -= CharacterAttack;
  }
  
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
    {
      animation.Play();
      audio.Play();
      other.GetComponent<Character>().Helth -= 10;
    }
  }

  private void CharacterAttack(int armo)
  {
    if (armo == 4)
    {
      Transform characterT = character.transform;
      Transform t = transform;
      float heigToChar = Mathf.Abs(t.position.y - characterT.position.y); //разница по высоте с персонажем
      float distToChar = Vector3.Distance(t.position, characterT.position);
      RaycastHit[] hits;
      hits = Physics.RaycastAll(t.position + Vector3.up * 0.1f, characterT.position - t.position, 5);
      int i = 0;
      float rayToChar = 100;
      while (i < hits.Length)
      {
        RaycastHit hit = hits[i];
        int collLayer = hit.collider.gameObject.layer;
        if (collLayer == 0) //Default layer
          rayToChar = Mathf.Min(hit.distance, rayToChar);
        i++;
      }

      bool notWall = distToChar < rayToChar;
      bool charPovernutRight = characterT.eulerAngles.y > 50 && characterT.eulerAngles.y < 120 && characterT.position.x - t.position.x < 0;
      //ГГ повернут вправо и монстр справа

      bool charPovernutLeft = characterT.eulerAngles.y > 230 && characterT.eulerAngles.y < 310 && characterT.position.x - t.position.x > 0;
      //ГГ повернут влево и монстр слева

      if (distToChar < delDist && charPovernutRight && heigToChar < delHeight && notWall)
      {
        //if (GameObject.Find(blastPrefab.name + "(Clone)") == null)
        //{
          Crash();
        //}

      }

      if (distToChar < delDist && charPovernutLeft && heigToChar < delHeight && notWall)
      {
        //if (GameObject.Find(blastPrefab.name + "(Clone)") == null)
        //{
          Crash();
        //}
      }
    }
  }

  public void Crash()
  {
    Instantiate(blastPrefab, transform.position + blastPos, transform.rotation);
    animation.Play();
    BoxCollider boxCollider = GetComponent<BoxCollider>();
    if (boxCollider != null)
      Destroy(boxCollider);
    boxCollider = GetComponent<BoxCollider>();
    if (boxCollider != null)
      Destroy(boxCollider);
  }
}
