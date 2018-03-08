using System;
using UnityEngine;

public class ThingTrigger : MonoBehaviour
{
  [SerializeField] private int id = 0;
  public event Action GetThing;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.name == "Stalker")
    {
      ArmoGUI[] buttons = FindObjectsOfType<ArmoGUI>();
      foreach (var armoGui in buttons)
      {
        if (armoGui.Id == id && armoGui.Id > 0)
          armoGui.ThingTriggerWritter(this);
      }
      var handler = GetThing;
      if (handler != null)
      handler();
      Destroy(gameObject);
    }
  }
}
