using UnityEngine;

public class Water : MonoBehaviour
{
  [SerializeField] private Material waterMaterial = null;
  private Vector2 offset;
  
	void Update ()
	{
	  offset.x += Time.deltaTime*0.1f;
    offset.y = Mathf.Sin(offset.x * 8) * 0.02f;
    waterMaterial.mainTextureOffset = offset;
    waterMaterial.SetTextureOffset("_Detail", new Vector2(offset.x * 0.5f, Mathf.Sin(offset.y * 6) * 0.02f));
	}
}
