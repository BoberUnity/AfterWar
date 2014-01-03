using UnityEngine;

public class TimeLive : MonoBehaviour 
{
  [SerializeField] private float liveTime = 4.9f;

	void Start () 
  {
    Destroy(gameObject, liveTime);
	}
}
