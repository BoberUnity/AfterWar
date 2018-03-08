using UnityEngine;

public class RigObject : MonoBehaviour 
{
  [SerializeField] private Character character = null;
  [SerializeField] private GameObject blastPrefab = null;
  [SerializeField] private float rpgFofce = 2000;
  [SerializeField] private bool destroyed = false;

  private void Start()
  {
    character.CharacterAttack += CharacterAttack;
  }

  private void OnDestroy()
  {
    character.CharacterAttack -= CharacterAttack;
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
      hits = Physics.RaycastAll(t.position + Vector3.up*0.1f, characterT.position - t.position, 5);
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

      if (distToChar < 2.5f && charPovernutRight && heigToChar < 0.35f && notWall)
      {
        GetComponent<Rigidbody>().AddForce(rpgFofce, 0, 0);
        if (GameObject.Find("BlastRPG(Clone)") == null)
          Instantiate(blastPrefab, t.position, t.rotation);
      }

      if (distToChar < 2.5f && charPovernutLeft && heigToChar < 0.35f && notWall)
      {
        GetComponent<Rigidbody>().AddForce(-rpgFofce, 0, 0);
        if (GameObject.Find("BlastRPG(Clone)") == null)
          Instantiate(blastPrefab, t.position, t.rotation);
      }
      if (destroyed)
        Destroy(gameObject);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 10)
      transform.parent = other.transform;
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.gameObject.layer == 10)
      transform.parent = null;
  }
}
