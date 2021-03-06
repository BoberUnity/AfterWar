﻿using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private UILabel fpsLabel = null;
  [SerializeField] private QualityEffect[] qualityEffects = null;

  private void Start()
  {
    //------------------------------------------------------------------------------------------


    //------------------------------------------------------------------------------------------
    if (QualitySettings.antiAliasing == 0 && id == 3)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.antiAliasing == 2 && id == 4)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.antiAliasing == 4 && id == 5)
      GetComponent<UIToggle>().value = true;

    //------------------------------------------------------------------------------------------
    if (QualitySettings.vSyncCount == 0 && id == 7)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.vSyncCount == 1 && id == 8)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.vSyncCount == 2 && id == 9)
      GetComponent<UIToggle>().value = true;
    //------------------------------------------------------------------------------------------
    if (QualitySettings.masterTextureLimit == 3 && id == 10)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.masterTextureLimit == 2 && id == 11)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.masterTextureLimit == 1 && id == 12)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.masterTextureLimit == 0 && id == 13)
      GetComponent<UIToggle>().value = true;

    if (id == 14)
    {
      GameObject controllerObj = GameObject.Find("Controller(Clone)");
      if (controllerObj != null)
      {
        GetComponent<UIToggle>().value = controllerObj.GetComponent<Controller>().ShowFps;
        if (fpsLabel != null)
          fpsLabel.enabled = GetComponent<UIToggle>().value;
      }
      else
      {
        Debug.LogWarning("Опция Показать ФПС недоступна, т.к. необходимо запустить сцену MenuMain");
      }
    }

    if (id == 0 || id == 1 || id == 2)
    {
      GameObject controllerObj = GameObject.Find("Controller(Clone)");
      if (controllerObj != null)
      {
        GetComponent<UIToggle>().value = controllerObj.GetComponent<Controller>().WaterHigh == id;
        //if (fpsLabel != null)
        //  fpsLabel.enabled = GetComponent<UIToggle>().value;
      }
    }
  }
  
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (id == 3)
      {
        QualitySettings.antiAliasing = 0;
        PlayerPrefs.SetInt("antiAliasing", 0);
      }
      if (id == 4)
      {
        QualitySettings.antiAliasing = 2;
        PlayerPrefs.SetInt("antiAliasing", 2);
      }
      if (id == 5)
      {
        QualitySettings.antiAliasing = 4;
        PlayerPrefs.SetInt("antiAliasing", 4);
      }


      if (id == 7)
      {
        QualitySettings.vSyncCount = 0;
        PlayerPrefs.SetInt("vSyncCount", 0);
      }
      if (id == 8)
      {
        QualitySettings.vSyncCount = 1;
        PlayerPrefs.SetInt("vSyncCount", 1);
      }
      if (id == 9)
      {
        QualitySettings.vSyncCount = 2;
        PlayerPrefs.SetInt("vSyncCount", 2);
      }

      if (id == 10)
      {
        QualitySettings.masterTextureLimit = 3;
        PlayerPrefs.SetInt("masterTextureLimit", 3);
      }
      if (id == 11)
      {
        QualitySettings.masterTextureLimit = 2;
        PlayerPrefs.SetInt("masterTextureLimit", 2);
      }
      if (id == 12)
      {
        QualitySettings.masterTextureLimit = 1;
        PlayerPrefs.SetInt("masterTextureLimit", 1);
      }
      if (id == 13)
      {
        QualitySettings.masterTextureLimit = 0;
        PlayerPrefs.SetInt("masterTextureLimit", 0);
      }

      if (id == 14)
      {
        GameObject.Find("Controller(Clone)").GetComponent<Controller>().ShowFps = !GetComponent<UIToggle>().value;
        if (fpsLabel != null)
        {
          bool showFps = !GetComponent<UIToggle>().value;
          fpsLabel.enabled = showFps;
          if (showFps)
            PlayerPrefs.SetInt("showFps", 1);
          else
            PlayerPrefs.SetInt("showFps", 0);
        }
      }

      if (id == 0 || id == 1 || id == 2)
      {
        GameObject obj = GameObject.Find("Controller(Clone)");
        if (obj != null)
          obj.GetComponent<Controller>().WaterHigh = id;
        PlayerPrefs.SetInt("waterHigh", id);
        foreach (var qe in qualityEffects)
        {
          qe.Quality = id;
        }
      }
    }
  }
}
