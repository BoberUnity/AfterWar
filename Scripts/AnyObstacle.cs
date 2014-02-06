using UnityEngine;

public class AnyObstacle : MonoBehaviour
{
  [SerializeField] private GameObject[] obstacles = null;

  private void Start () 
  {
    GameObject go = Instantiate(obstacles[Mathf.RoundToInt(Random.value * obstacles.Length - 0.5f)], transform.position, Quaternion.identity) as GameObject;
    if (go != null)
      go.transform.parent = transform;
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, 0.15f);
  }
}
