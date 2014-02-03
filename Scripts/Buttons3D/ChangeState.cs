using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class ChangeState : Button3DBase
  {
    [SerializeField] private int val = 0;
    private int state = 0;
    public int State
    {
      get { return state; }
      set { state = value; }
    }

    protected override void MakeAction()
    {
      state = val;
    }
  }
}
