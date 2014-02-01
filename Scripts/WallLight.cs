using UnityEngine;

public class WallLight : MonoBehaviour
{
  [SerializeField] private Material lightMaterial = null;
  [SerializeField] private float speed = 1;
  [SerializeField] private float maxA = 1;
  [SerializeField] private float minA = 0;
  private bool on = false;
  private bool up = false;
  private float a = 0;

  public bool On
  {
    set { on = value;}
  }

	void Start () 
  {
    lightMaterial.color = new Color(1, 1, 0.5f, minA);
	}
	
	void Update () 
  {
    if (on)
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
        a -= Time.deltaTime*speed;
        if (a < minA)
        {
          up = true;
          a = minA;
        }
      }
      lightMaterial.color = new Color(1, 1, 0.5f, a);
    }
  }
}
