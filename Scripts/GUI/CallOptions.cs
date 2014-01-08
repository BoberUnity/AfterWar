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
        aObjs.SetActive(true);
      }
      foreach (var aObjs in deactiveObjs)
      {
        aObjs.SetActive(false);
      }
    }
	}
}
