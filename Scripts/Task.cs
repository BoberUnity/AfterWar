using UnityEngine;

public class Task : MonoBehaviour
{
  [SerializeField] private int etap = 0;

  public int Etap
  {
    get {return etap;}
    set { etap = value; }
  }
}
