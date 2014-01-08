using UnityEngine;

public class OptionsSetter : MonoBehaviour
{
  [SerializeField] private float v;
  private UISlider slider = null;
  private Controller controller = null;

	private void Start ()
	{
	  slider = GetComponent<UISlider>();
	  GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
    {
      controller = obj.GetComponent<Controller>();
      slider.value = controller.EffectsVolume;
    }
    else Debug.LogWarning("Controller(Clone) was not found!");
	}
	
  private void Update ()
	{
	  controller.EffectsVolume = slider.value;
	}
}
