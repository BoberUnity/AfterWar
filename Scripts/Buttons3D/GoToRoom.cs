using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Buttons3D
{
  public class GoToRoom : Button3DBase
  {
    [SerializeField] private float waitTime = 0;
    [SerializeField] private float y = 1;
    [SerializeField] private float minX = -100;
    [SerializeField] private float maxX = 100;
    [SerializeField] private float minY = -100;
    [SerializeField] private float maxY = 100;
    [SerializeField] private float camDist = 1.4f;
    [SerializeField] private float minDist = 0.7f;
    [SerializeField] private float maxDist = 2;
    [SerializeField] private float camHeight = 0.5f;
    [SerializeField] private bool follow = false;
    [SerializeField] private float charRotY = 270;
    private bool isLoaded = false;

    protected override void MakeAction()
    {
      if (!isLoaded)
      {
        StartCoroutine(LoadLevel(waitTime));
      }
    }

    private IEnumerator LoadLevel(float time)
    {
      yield return new WaitForSeconds(time);
      CameraController cameraController = character.GetComponent<CameraController>();
      cameraController.MinX = minX;
      cameraController.MaxX = maxX;
      cameraController.MinY = minY;
      cameraController.MaxY = maxY;
      cameraController.CamDist = camDist;
      cameraController.MinDist = minDist;
      cameraController.MaxDist = maxDist;
      cameraController.CamHeight = camHeight;
      cameraController.Follow = follow;
      cameraController.CamTrans.position = new Vector3(cameraController.CamTrans.position.x, Mathf.Clamp(y + camHeight, minY, maxY), -camDist);
      character.transform.position = new Vector3(transform.position.x, y, 0);
      character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x, charRotY, character.transform.eulerAngles.z);
    }
  }
}
