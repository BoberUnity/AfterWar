using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
    public class DisableObject : Button3DBase
  {
    [SerializeField] private GameObject disableObj = null;
    [SerializeField] private GameObject enableObj = null;

    protected override void MakeAction()
    {
        Destroy(disableObj);
        if (enableObj != null)
            enableObj.SetActive(true);
    }
  }
}
