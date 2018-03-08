using UnityEngine;
using System.Collections;

public class PlatformStop : MonoBehaviour
{
  private Transform characterTransform = null;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
    {
      characterTransform = other.transform;
      if (characterTransform.position.y > 0.6f)
      {
        GetComponent<Animation>().Stop();
        StartCoroutine(AgainMove(4));
      }
    }
  }
  //Снова запустим платформу, ежели перс внизу
  private IEnumerator AgainMove(float time)
  {
    yield return new WaitForSeconds(time);
    if (characterTransform.position.y > 0.6f)
      StartCoroutine(AgainMove(4));
    else 
      GetComponent<Animation>().Play();
  }
}
