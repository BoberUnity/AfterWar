using UnityEngine;

public class CallOptions : MonoBehaviour 
{
  [SerializeField] private GameObject[] activeObjs = null;
  [SerializeField] private GameObject[] deactiveObjs = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      foreach (var aObjs in activeObjs)
      {
        if (aObjs != null)
          aObjs.SetActive(true);
      }
      foreach (var aObjs in deactiveObjs)
      {
        if (aObjs != null) 
          aObjs.SetActive(false);
      }
    }
	}
}
