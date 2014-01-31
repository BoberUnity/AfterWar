using UnityEngine;

public class QualityEffect : MonoBehaviour
{
  [SerializeField] private GameObject low = null;
  [SerializeField] private GameObject middle = null;
  [SerializeField] private GameObject high = null;
  [SerializeField] private int thisQuality = 0;//1-актив себя на ср, 2 - на выс
  private int quality = 0;

  public int Quality
  {
    set
    {
      quality = value;
      SelectObject();
    }
  }

	private void Start ()
	{
	  GameObject obj = GameObject.Find("Controller(Clone)");
    if (obj != null)
      quality = obj.GetComponent<Controller>().WaterHigh;
	  SelectObject();
	}
	
	private void SelectObject() 
  {
    if (thisQuality == 0)
    {
      if (quality == 0)
      {
        low.SetActive(true);
        middle.SetActive(false);
        high.SetActive(false);
      }

      if (quality == 1)
      {
        low.SetActive(false);
        middle.SetActive(true);
        high.SetActive(false);
      }

      if (quality == 2)
      {
        low.SetActive(false);
        middle.SetActive(false);
        high.SetActive(true);
      }
    }
    else
    {
      if (quality == 0)
        gameObject.SetActive(false);
      if (quality > 0 && thisQuality == 1)
        gameObject.SetActive(true);
      if (quality == 2 && thisQuality == 2)
        gameObject.SetActive(true);
    }
	}
}
