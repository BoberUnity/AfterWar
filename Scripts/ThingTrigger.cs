using System;
using UnityEngine;

public class ThingTrigger : MonoBehaviour
{
  public event Action GetThing;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer == 8)
    {
      var handler = GetThing;
      if (handler != null)
      handler();
      Destroy(gameObject);
    }
  }
}
