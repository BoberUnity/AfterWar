using UnityEngine;

public class LightIntensit : MonoBehaviour 
{
	void Start () 
  {
    GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
    {
      Controller controller = obj.GetComponent<Controller>();
      if (light != null)
        light.intensity = 2.5f + controller.ScreenBright*5.5f;
    }
  }
}
