using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
  public int id = 0;

  private void Start()
  {
    
  }
  
  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (id == 0)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
      if (id == 1)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
      if (id == 2)
        QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

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
    }
  }
}
