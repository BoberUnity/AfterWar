using UnityEngine;public class CreatorOnce : MonoBehaviour{  [SerializeField] private GameObject prefab = null;  private void Start()  {    if (GameObject.Find(prefab.name + "(Clone)") == null)    {      GameObject controller = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

      QualitySettings.antiAliasing = PlayerPrefs.GetInt("antiAliasing");
      QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSyncCount");
      QualitySettings.masterTextureLimit = PlayerPrefs.GetInt("masterTextureLimit");
      
      if (controller != null)
      {
        int sf = PlayerPrefs.GetInt("showFps");
        if (sf == 0)
          controller.GetComponent<Controller>().ShowFps = false;
        else
          controller.GetComponent<Controller>().ShowFps = true;
        //Громкость эффектов
        float vol = PlayerPrefs.GetFloat("effectsVolume");        if (vol < 0.001f)
        {
          controller.GetComponent<Controller>().EffectsVolume = 0.75f; 
          Debug.Log("First start");
        }
        else
          controller.GetComponent<Controller>().EffectsVolume = vol;
        //Громкость музыки
        vol = PlayerPrefs.GetFloat("musicVolume");
        if (vol < 0.001f)
        {
          controller.GetComponent<Controller>().MusicVolume = 0.75f;
        }
        else
          controller.GetComponent<Controller>().MusicVolume = vol;
        //Яркость экрана
        vol = PlayerPrefs.GetFloat("screenBright");
        if (vol < 0.001f)
        {
          controller.GetComponent<Controller>().ScreenBright = 0.75f;
        }
        else
          controller.GetComponent<Controller>().ScreenBright = vol;        //Effects
        controller.GetComponent<Controller>().WaterHigh = PlayerPrefs.GetInt("waterHigh");      }
      else
      {
        Debug.LogWarning("controller was not found");
      }    }    else    {      Debug.Log("Controller was not created");    }    Destroy(gameObject);  }}