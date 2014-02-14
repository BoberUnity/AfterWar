using Assets.Scripts.Buttons3D;
using UnityEngine;

public class OpenDoorOtherLevel : Button3DBase
{
  [SerializeField] private OtherLevel otherLevel = null;

  protected override void MakeAction()
  {
    otherLevel.Open = true;
  }
}
