using UnityEngine;

public class ButtonLoadLevelFast : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private bool restart = false;


  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (restart)
      { //Сброс оружия и вещей 
        GameObject obj = GameObject.Find("Controller(Clone)");
        if (obj != null)
        {
          Controller controller = obj.GetComponent<Controller>();
          controller.Reset();
        }
      }
      Application.LoadLevel(id);
      Time.timeScale = 1;
    }
  }
}
