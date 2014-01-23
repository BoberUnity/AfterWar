using System.Collections;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
  [SerializeField] private int id = 0;
  [SerializeField] private UILabel loadingText = null;
  [SerializeField] private UILabel loadingProgress = null;
  private AsyncOperation async;
  private bool loading = false;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && !loading)
    {
      if (loadingText != null)
        loadingText.gameObject.SetActive(true);
      if (loadingProgress != null)
        loadingProgress.gameObject.SetActive(true);
      StartCoroutine(Load());
      loading = true;
    }
  }

  //private IEnumerator Load(float time)
  //{
  //  yield return new WaitForSeconds(time);
  //  Application.LoadLevel(id);
  //}

  private void Update()
  {
    if (loadingProgress != null && async != null)
    {
      loadingProgress.text = (async.progress * 100).ToString("f0") + " %";
      //loadAnim[loadAnim.clip.name].time = async.progress;
    }
  }

  private IEnumerator Load()
  {
    async = Application.LoadLevelAsync(id);
    yield return async;
  }
}



  

  

