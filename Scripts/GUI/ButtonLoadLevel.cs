using System.Collections;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private UILabel loadingText = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      if (loadingText != null)
        loadingText.gameObject.SetActive(true);
      StartCoroutine(Load(0.001f));
    }
  }

  private IEnumerator Load(float time)
  {
    yield return new WaitForSeconds(time);
    Application.LoadLevel(id);
  }
}
