using UnityEngine;

public class ButtonObjOnOff : MonoBehaviour
{
  [SerializeField] private GameObject obj = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      obj.SetActive(!obj.activeSelf);
    }
  }
}
