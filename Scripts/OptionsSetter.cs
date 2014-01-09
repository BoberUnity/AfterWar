using UnityEngine;

[RequireComponent(typeof(UIProgressBar))]

public class OptionsSetter : MonoBehaviour
{
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
      progressBar.value = controller.EffectsVolume;
    }
    else Debug.LogWarning("Controller не создан, необходимо запустить сценгу MtnuMain");
	}

  private void OnDestroy()
  {
    progressBar.ChangeValue -= ChangeValue;
  }
	
  private void ChangeValue(float value)
  {
    controller.EffectsVolume = value;
    PlayerPrefs.SetFloat("effectsVolume", Mathf.Max(0.01f, value));
	}
}
