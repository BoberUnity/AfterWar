using UnityEngine;

public class QualityEffect : MonoBehaviour
{
  [SerializeField] private GameObject low = null;
  [SerializeField] private GameObject middle = null;
  [SerializeField] private GameObject high = null;
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
}
