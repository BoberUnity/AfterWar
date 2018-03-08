using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class OtherLevel : Button3DBase
  {
    [SerializeField] private int id = 1;
    [SerializeField] private int num = 1;
    [SerializeField] private float waitTime = 0;
    [SerializeField] private bool open = true;
    public bool Open
    {
      set { open = value; }
    }

    private bool isLoaded = false;

    protected override void MakeAction()
    {
      if (!isLoaded && open)
      {
        StartCoroutine(LoadLevel(waitTime));
        PlayerPrefs.SetInt("Level", num);
      }
    }

    private IEnumerator LoadLevel(float time)
    {
      yield return new WaitForSeconds(time);

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
