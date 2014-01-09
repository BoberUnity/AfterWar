using UnityEngine;

public class OptionsSetter : MonoBehaviour
{
  //[SerializeField] private float v;
  private UISlider slider = null;
  private Controller controller = null;
  private float vol = -1;

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
	
  private void Update()
  {
    //if (!isPressed)
    //{
    //if (vol != controller.EffectsVolume)//Сдвинул ползунок
    //{
      //Debug.LogWarning("Upgrade"+Time.time);
      vol = Mathf.Max(0.01f, controller.EffectsVolume);
      controller.EffectsVolume = slider.value;
      PlayerPrefs.SetFloat("effectsVolume", vol);
    //}
	}
}
