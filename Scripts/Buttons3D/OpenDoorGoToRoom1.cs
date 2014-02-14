using Assets.Scripts.Buttons3D;
using UnityEngine;

public class OpenDoorGoToRoom : Button3DBase
{
  [SerializeField] private GoToRoom goToRoom = null;

  protected override void MakeAction()
  {
    goToRoom.Open = true;
  }
}
