using UnityEngine;

public class Task : MonoBehaviour
{
  private int etap = 0;

  public int Etap
  {
    get {return etap;}
    set { etap = value; }
  }
}
