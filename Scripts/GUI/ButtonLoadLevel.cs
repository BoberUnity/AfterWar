using System.Collections;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private UILabel loadingText = null;
  private bool loading = false;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && !loading)
    {
      if (loadingText != null)
        loadingText.gameObject.SetActive(true);
      StartCoroutine(Load(0.001f));
      loading = true;
    }
  }

  private IEnumerator Load(float time)
  {
    yield return new WaitForSeconds(time);
    Application.LoadLevel(id);
  }
}
