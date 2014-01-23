using UnityEngine;

public class GazScreen : MonoBehaviour
{
  [SerializeField] private GameObject camObj = null;
	
	private void Update()
  {
    transform.position = camObj.transform.position + Vector3.forward*0.25f;
  }
}
