using System;
using UnityEngine;

public class Task1 : MonoBehaviour
{
  [Serializable] private class Stage
  {
    [SerializeField] public GameObject[] activeObjs = null;
    [SerializeField] public GameObject[] deactiveObjs = null;
  }

  [SerializeField] private Task task = null;
  [SerializeField] private Stage[] stages = null;
  
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      foreach (var aObjs in stages[task.Etap].activeObjs)
      {
        if (aObjs != null)
          aObjs.SetActive(true);
      }
      foreach (var aObjs in stages[task.Etap].deactiveObjs)
      {
        if (aObjs != null) 
          aObjs.SetActive(false);
      }
    }
	}
}
