using UnityEngine;

[RequireComponent(typeof(UIProgressBar))]

public class OptionsSetter : MonoBehaviour
{
  [SerializeField] private int id = 0;//0 effects; 1 music; 2-screen
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
        progressBar.value = controller.ScreenBright;
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
      controller.ScreenBright = value;
      PlayerPrefs.SetFloat("screenBright", Mathf.Max(0.01f, value));
    }
	}
}
