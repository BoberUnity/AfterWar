using System;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
  [SerializeField] private int id = 0;
  //[SerializeField] private UILabel loadingText = null;
  //[SerializeField] private UILabel loadingProgress = null;
  //[SerializeField] private GameObject startButton = null;
  private AsyncOperation async;
  private bool loading = false;
  private bool startButtonActive = false;
  
  public event Action<int> StartLoadLevel;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed && !loading)
    {
      //if (loadingText != null)
      //  loadingText.gameObject.SetActive(true);
      //if (loadingProgress != null)
      //  loadingProgress.gameObject.SetActive(true);
      //StartCoroutine(Load());
      var handler = StartLoadLevel;
      if (handler != null)
        handler(id);
      loading = true;
    }
  }

  ////private IEnumerator Load(float time)
  ////{
  ////  yield return new WaitForSeconds(time);
  ////  Application.LoadLevel(id);
  ////}

  //private void Update()
  //{
  //  if (loadingProgress != null && async != null)
  //  {
  //    loadingProgress.text = (async.progress * 100).ToString("f0") + " %";
  //    if (async.progress > 0.99999f && startButton != null)
  //    {
  //      if (loadingText != null)
  //        loadingText.gameObject.SetActive(false);
  //      if (loadingProgress != null)
  //        loadingProgress.gameObject.SetActive(false);
  //      startButton.SetActive(true);
  //    }
  //    //loadAnim[loadAnim.clip.name].time = async.progress;
  //  }
  //}

  //private IEnumerator Load()
  //{
  //  async = Application.LoadLevelAsync(id);
  //  yield return async;
  //}
}



  

  

