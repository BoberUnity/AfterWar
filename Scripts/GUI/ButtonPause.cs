using UnityEngine;

public class ButtonPause : MonoBehaviour
{
  [SerializeField] private UIPanel pauseMenu = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {  
      if (Time.timeScale < .1f)
        Time.timeScale = 1;
      else 
        Time.timeScale = 0;

      pauseMenu.gameObject.SetActive(Time.timeScale < .1f);
    }
  }

}
