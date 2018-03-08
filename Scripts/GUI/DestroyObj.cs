using UnityEngine;

public class DestroyObj : MonoBehaviour 
{
  [SerializeField] private GameObject[] destroyObjs = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
        foreach (var dObj in destroyObjs)
      {
          if (dObj != null)
              Destroy(dObj);
      }
    }
	}
}
