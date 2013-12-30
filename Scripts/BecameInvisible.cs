using System;
using UnityEngine;

public class BecameInvisible : MonoBehaviour
{
  public event Action ExtiRender;
  //--------------------------------------------------------------------------------------------------
  private void OnBecameInvisible()
  {
    //Debug.Log(" OnBecameInvisible");
    var handler = ExtiRender;
    if (handler != null)
      handler();
  }
}
