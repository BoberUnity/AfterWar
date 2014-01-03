using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class OtherLevel : Button3DBase
  {
    [SerializeField] private int id = 1;

    private bool isLoaded = false;

    protected override void MakeAction()
    {
      if (!isLoaded)
      {
        GameObject obj = GameObject.Find("Controller(Clone)");
        if (obj != null)
        {
          Controller controller = obj.GetComponent<Controller>();
          controller.Patrons = character.Patrons;
          controller.CurrentArmo = character.CurrentArmo;
          controller.Helth = character.Helth;
        }
        else
          Debug.LogWarning("Controller(Clone) was not found");
        
        Application.LoadLevel(id);
      }
    }
  }
}
