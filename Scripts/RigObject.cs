using UnityEngine;

public class RigObject : MonoBehaviour {

  [SerializeField] private Character character = null;
  [SerializeField] private GameObject blastPrefab = null;
  [SerializeField] private float rpgFofce = 2000;

  private void Start()
  {
    character.CharacterAttack += CharacterAttack;
  }

  private void CharacterAttack(int armo)
  {
    if (armo == 4)
    {
      Debug.LogWarning(gameObject.name);
      Transform characterT = character.transform;
      Transform t = transform;
      float heigToChar = Mathf.Abs(t.position.y - characterT.position.y); //разница по высоте с персонажем
      Debug.Log("heigToChar " + heigToChar);
      float distToChar = Vector3.Distance(t.position, characterT.position);
      Debug.Log("distToChar " + distToChar);
      RaycastHit[] hits;
      //hits = Physics.RaycastAll(t.position + Vector3.up * (0.2f - height), Vector3.right * Mathf.Sign(characterT.position.x - t.position.x), 5);
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
      Debug.Log("rayToChar " + rayToChar);
      bool notWall = distToChar < rayToChar;
      Debug.Log("notWall " + notWall);

      bool charPovernutRight = false;
      if (characterT.eulerAngles.y > 50 && characterT.eulerAngles.y < 120 && characterT.position.x - t.position.x < 0)
        //ГГ повернут вправо и монстр справа
        charPovernutRight = true;
      bool charPovernutLeft = false;
      if (characterT.eulerAngles.y > 230 && characterT.eulerAngles.y < 310 && characterT.position.x - t.position.x > 0)
        //ГГ повернут влево и монстр слева
        charPovernutLeft = true;

      //if (distToChar < 0.3f && character.NearMonstr < 2)
      //  charPovernut = true;
      if (distToChar < 2.5f && charPovernutRight && heigToChar < 0.35f && notWall)
      {
        rigidbody.AddForce(rpgFofce, 0, 0);
        if (GameObject.Find("BlastRPG(Clone)") == null)
        {
          Instantiate(blastPrefab, t.position, t.rotation);
          Debug.LogWarning("INSTANCE ");
        }
        else
        {
          Debug.LogWarning("DO NOT INSTANCE ");
        }
      }
      if (distToChar < 2.5f && charPovernutLeft && heigToChar < 0.35f && notWall)
      {
        rigidbody.AddForce(-rpgFofce, 0, 0);
        if (GameObject.Find("BlastRPG(Clone)") == null)
        {
          Instantiate(blastPrefab, t.position, t.rotation);
          Debug.LogWarning("INSTANCE ");
        }
        else
        {
          Debug.LogWarning("DO NOT INSTANCE ");
        }
      }

      
    }
  }
}
