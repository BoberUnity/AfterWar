using UnityEngine;

[RequireComponent(typeof(UIProgressBar))]

public class OptionsSetter : MonoBehaviour
{
  [SerializeField] private int id = 0;//0 effects; 1 music; 2-screen
  [SerializeField] private Light light = null;
  //[SerializeField] private BrightEffect brightEffect = null;
  private UIProgressBar progressBar = null;
  private Controller controller = null;

	private void Start ()
	{
	  progressBar = GetComponent<UIProgressBar>();
    progressBar.ChangeValue += ChangeValue;
	  GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
    {
      controller = obj.GetComponent<Controller>();
      if (id == 0)
        progressBar.value = controller.EffectsVolume;
      if (id == 1)
        progressBar.value = controller.MusicVolume;
      if (id == 2)
      {
        progressBar.value = controller.ScreenBright;
        if (light != null)
          light.intensity = progressBar.value*2;
          //light.intensity = 2.5f + progressBar.value * 5.5f;
        //brightEffect.Bright = progressBar.value;
      }
      
    }
    else Debug.LogWarning("Controller не создан, необходимо запустить сценгу MtnuMain");
	}

  private void OnDestroy()
  {
    progressBar.ChangeValue -= ChangeValue;
  }
	
  private void ChangeValue(float value)
  {
    if (id == 0)
    {
      controller.EffectsVolume = value;
      PlayerPrefs.SetFloat("effectsVolume", Mathf.Max(0.01f, value));
    }
    if (id == 1)
    {
      controller.MusicVolume = value;
      PlayerPrefs.SetFloat("musicVolume", Mathf.Max(0.01f, value));
    }
    if (id == 2)
    {
      if (controller != null) 
        controller.ScreenBright = value;
      PlayerPrefs.SetFloat("screenBright", Mathf.Max(0.01f, value));
      if (light != null)
        light.intensity = value*2;
      //brightEffect.Bright = value;
    }
	}
}
