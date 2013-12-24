using UnityEngine;


public class BackgroundScale : MonoBehaviour
{
  [SerializeField] private float scaleY = 0.205f;

	void Start () 
  {
    transform.localScale = new Vector3(scaleY*Screen.width/Screen.height, 1, scaleY);
	}
}
