using UnityEngine;

public class ButtonObjOnOff : MonoBehaviour
{
  [SerializeField] private GameObject obj = null;
  [SerializeField] private bool paused = false;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      obj.SetActive(!obj.activeSelf);
    }

    if (paused)
    {
      if (!obj.activeSelf)
        Time.timeScale = 1;
      else 
        Time.timeScale = 0;
    }
  }
}
