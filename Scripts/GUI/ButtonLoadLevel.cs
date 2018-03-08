using System;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
  [SerializeField] private int id = 0;//Scene
  [SerializeField] private int num = 0;//Poradok
  [SerializeField] private UIButton uIButton = null;
  [SerializeField] private UISprite sprite = null;
  [SerializeField] private UILabel label = null;
  private bool loading = false;

  private void OnEnable()
  {
      uIButton.isEnabled = PlayerPrefs.GetInt("Level") >= num;
      sprite.color = PlayerPrefs.GetInt("Level") >= num ? Color.white : new Color(0.10f, 0.15f, 0.25f, 1);
      label.color = PlayerPrefs.GetInt("Level") >= num ? Color.white : new Color(0.15f, 0.25f, 0.35f, 1);
  }
  
  public event Action<int> StartLoadLevel;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && !loading)
    {
      var handler = StartLoadLevel;
      if (handler != null)
        handler(id);
      loading = true;
    }
  }  
}



  

  

