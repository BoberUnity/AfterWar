using UnityEngine;

public class PlayAnim2D: MonoBehaviour 
{
  [SerializeField] private Animation anim = null;

  protected virtual void OnPress(bool isPressed)
  {
    if (!isPressed)
    {
      anim.Play();
    }
	}
}
