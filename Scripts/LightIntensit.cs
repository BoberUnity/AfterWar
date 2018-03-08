using UnityEngine;

public class LightIntensit : MonoBehaviour 
{
	void Start () 
  {
    GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
    {
      Controller controller = obj.GetComponent<Controller>();
      if (GetComponent<Light>() != null)
        GetComponent<Light>().intensity = controller.ScreenBright*2;
    }
  }
}
