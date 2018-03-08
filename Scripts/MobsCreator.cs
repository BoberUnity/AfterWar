using UnityEngine;

public class MobsCreator : MonoBehaviour
{
  [SerializeField] private GameObject mob = null;
  private GameObject currentMob = null;

  private void OnBecameVisible()
  {
    if (currentMob == null)
      currentMob = Instantiate(mob, transform.position, transform.rotation) as GameObject;
  }

  //private void OnBecameInvisible()
  //{
  //  Debug.Log(" OnBecameInVisible");
  //}

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(transform.position, 0.05f);
  }
}
