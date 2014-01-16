using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/BrightEffect")]
public class BrightEffect : ImageEffectBase 
{
  [SerializeField] private float bright = 0.5f;

  public float Bright
  {
    set { bright = value;}
  }

  private void Start()
  {
    GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
    {
      Controller controller = obj.GetComponent<Controller>();
      bright = controller.ScreenBright;
    }
  }

	private void OnRenderImage (RenderTexture source, RenderTexture destination) 
  {
    material.SetFloat("_Mtl", bright);
		Graphics.Blit (source, destination, material);
	}
}
