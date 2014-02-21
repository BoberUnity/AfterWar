using UnityEngine;

public class Teleport3D: Button3DBase
{
  [SerializeField] private Vector3 pos;

  protected override void MakeAction()
  {
    character.transform.position = pos;
  }
}
