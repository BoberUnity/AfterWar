using UnityEngine;

public class LightAlpha : MonoBehaviour
{
  [SerializeField] private Material lightMaterial = null;
  [SerializeField] private float speed = 1;
  [SerializeField] private float maxA = 1;
  [SerializeField] private float minA = 0;
  private bool up = false;
  private float a = 0;

	void Start () 
  {
	
	}
	
	void Update () 
  {
	  if (up)
	  {
	    a += Time.deltaTime*speed;
      if (a > maxA)
      {
        up = false;
        a = maxA;
      }
	  }
    else
	  {
	    a -= Time.deltaTime * speed;
      if (a < minA)
      {
        up = true;
        a = minA;
      }
	  }
    lightMaterial.color = new Color(1, 1, 0.5f, a);
	}
}
