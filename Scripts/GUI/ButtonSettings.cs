using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private UILabel fpsLabel = null;

  private void Start()
  {
    //------------------------------------------------------------------------------------------
    if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Disable && id == 0)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.Enable && id == 1)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.anisotropicFiltering == AnisotropicFiltering.ForceEnable && id == 2)
      GetComponent<UIToggle>().value = true;

    //------------------------------------------------------------------------------------------
    if (QualitySettings.antiAliasing == 0 && id == 3)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.antiAliasing == 2 && id == 4)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.antiAliasing == 4 && id == 5)
      GetComponent<UIToggle>().value = true;

    if (QualitySettings.antiAliasing == 8 && id == 6)
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
      GetComponent<UIToggle>().value = GameObject.Find("Controller(Clone)").GetComponent<Controller>().ShowFps;
      if (fpsLabel != null)
        fpsLabel.enabled = GetComponent<UIToggle>().value;
    }
  }
  
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (id == 0)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
      if (id == 1)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
      if (id == 2)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;

      if (id == 3)
        QualitySettings.antiAliasing = 0;
      if (id == 4)
        QualitySettings.antiAliasing = 2;
      if (id == 5)
        QualitySettings.antiAliasing = 4;
      if (id == 6)
        QualitySettings.antiAliasing = 8;

      if (id == 7)
        QualitySettings.vSyncCount = 0;
      if (id == 8)
        QualitySettings.vSyncCount = 1;
      if (id == 9)
        QualitySettings.vSyncCount = 2;

      if (id == 10)
        QualitySettings.masterTextureLimit = 3;
      if (id == 11)
        QualitySettings.masterTextureLimit = 2;
      if (id == 12)
        QualitySettings.masterTextureLimit = 1;
      if (id == 13)
        QualitySettings.masterTextureLimit = 0;

      if (id == 14)
      {
        GameObject.Find("Controller(Clone)").GetComponent<Controller>().ShowFps = !GetComponent<UIToggle>().value;
        if (fpsLabel != null)
          fpsLabel.enabled = !GetComponent<UIToggle>().value;
      }
    }
  }
}
