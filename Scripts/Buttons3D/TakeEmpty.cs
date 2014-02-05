using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class TakeEmpty: Button3DBase
  {
    [SerializeField] private GameObject activeObj = null;
    [SerializeField] private GameObject deactiveObj = null;

    protected override void MakeAction()
    {
      //foreach (var aObjs in activeObjs)
      //{
      //  if (aObjs != null)
      //    aObjs.SetActive(true);
      //}
      //foreach (var aObjs in deactiveObjs)
      //{
      //  if (aObjs != null)
      //    aObjs.SetActive(false);
      //}
      activeObj.SetActive(true);
      deactiveObj.SetActive(false);
      Destroy(gameObject);
    }
  }
}
