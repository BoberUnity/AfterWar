using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class WallLighttOn : Button3DBase
  {
    [SerializeField] private WallLight[] lamps = null;

    protected override void MakeAction()
    {
      foreach (var l in lamps)
      {
        l.On = true;
      }
    }
  }
}
