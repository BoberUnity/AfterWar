using UnityEngine;

public class Lamp : MonoBehaviour
{
  [SerializeField] private Material redMat = null;
  [SerializeField] private Material greenMat = null;
  [SerializeField] private GameObject redFlare;
  [SerializeField] private GameObject greenFlare;
  private int state = 0; //1 red; 2 green
	
  //private void OnEnable()
  //{
  //  greenFlare.SetActive(state > 1);
  //  redFlare.SetActive(state > 0);
  //}
  
  public int State
  {
    get { return state; }
    set 
    {
      if (value == 2 && state == 1)
      {
        //Green
        renderer.material = greenMat;
        greenFlare.SetActive(true);
        redFlare.SetActive(false);
        state = value;
      }

      if (value == 1 && state == 0)
      {
        //Red
        renderer.material = redMat;
        greenFlare.SetActive(false);
        redFlare.SetActive(true);
        state = value;
      }
    }
  }
}
