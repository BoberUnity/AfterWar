using UnityEngine;

public class CreatorOnce : MonoBehaviour
{
  [SerializeField] private GameObject prefab = null;

  private void Start()
  {
    if (GameObject.Find(prefab.name + "(Clone)") == null)
    {
      Instantiate(prefab, Vector3.zero, Quaternion.identity);
      Debug.Log("Controller created");
    }
    else
    {
      Debug.Log("Controller was not created");
    }
    Destroy(gameObject);
  }
}
