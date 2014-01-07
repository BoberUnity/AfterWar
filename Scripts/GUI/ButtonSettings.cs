using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
  public int id = 0;
  
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
    }
  }
}
