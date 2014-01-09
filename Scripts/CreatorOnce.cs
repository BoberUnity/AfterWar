using UnityEngine;public class CreatorOnce : MonoBehaviour{  [SerializeField] private GameObject prefab = null;  private void Start()  {    if (GameObject.Find(prefab.name + "(Clone)") == null)    {      GameObject controller = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

      int aaf = PlayerPrefs.GetInt("anisotropicFiltering");
      if (aaf == 0)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
      if (aaf == 1)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
      if (aaf == 2)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;      QualitySettings.antiAliasing = PlayerPrefs.GetInt("antiAliasing");
      QualitySettings.vSyncCount = PlayerPrefs.GetInt("vSyncCount");
      QualitySettings.masterTextureLimit = PlayerPrefs.GetInt("masterTextureLimit");
      int sf = PlayerPrefs.GetInt("showFps");
      if (controller != null)
      {
        if (sf == 0)
          controller.GetComponent<Controller>().ShowFps = false;
        else
          controller.GetComponent<Controller>().ShowFps = true;      }
      else
      {
        Debug.LogWarning("controller was not found");
      }    }    else    {      Debug.Log("Controller was not created");    }    Destroy(gameObject);  }}