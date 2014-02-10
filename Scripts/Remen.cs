using UnityEngine;

public class Remen : MonoBehaviour
{
  [SerializeField] private Material mat = null;
  [SerializeField] private float speed = 1;
  private Vector2 offset = Vector2.zero;
  private bool on = false;
  public bool On
  {
    set { on = value; }
  }

	void Update ()
	{
	  if (on)
    {
      offset.y += Time.deltaTime*speed;
      renderer.material.SetTextureOffset("_MainTex", offset);
    }
	}
}
