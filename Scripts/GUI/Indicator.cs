using UnityEngine;

[RequireComponent(typeof(UISprite))]

public class Indicator : MonoBehaviour
{
  [SerializeField] private UISprite sprite = null;
  [SerializeField] private UILabel label = null;
  [SerializeField] private bool offIfZero = false;
  private UISprite thisSprite = null;
  private float val = 100;

  public float Val
  {
    get { return val; }
    set 
    { 
      val = Mathf.Clamp(value, 0, 100);

      if (offIfZero)
      {
        SetState(val > 1);
      }

      sprite.width = (int)val;
      sprite.transform.localPosition = new Vector3(-50 + val / 2, 0, 0);
      if (label != null)
        label.text = val.ToString("f0") + "%";
    }
  }

  private void Awake()
  {
    thisSprite = GetComponent<UISprite>();
  }

  private void SetState(bool on)
  {
    thisSprite.enabled = on;
    sprite.enabled = on;
  }
}
