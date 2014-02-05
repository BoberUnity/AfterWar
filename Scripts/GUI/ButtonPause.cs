using UnityEngine;

public class ButtonPause : MonoBehaviour
{
  [SerializeField] private UIPanel pauseMenu = null;
  [SerializeField] private GameObject buttonGame = null;
  [SerializeField] private GameObject[] deactiveObjs = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      SetPause();
    }
  }

  private void SetPause()
  {
    if (Time.timeScale < .1f)
      Time.timeScale = 1;
    else
      Time.timeScale = 0;

    pauseMenu.gameObject.SetActive(Time.timeScale < .1f);

    foreach (var aObjs in deactiveObjs)
    {
      if (aObjs != null)
        aObjs.SetActive(false);
    }
  }

  public void Dead()
  {
    SetPause();
    buttonGame.SetActive(false);
  }
}
