using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class LampColor : Button3DBase
  {
    [SerializeField] private Lamp lamp = null;
    [SerializeField] private int state = 1;

    protected override void MakeAction()
    {
      lamp.State = state;
      Debug.Log("Press 3D Button");
    }
  }
}
