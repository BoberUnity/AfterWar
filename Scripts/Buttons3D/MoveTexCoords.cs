using UnityEngine;

public class MoveTexCoords : Button3DBase
{
  [SerializeField] private Material mat = null;
  [SerializeField] private float speed = 1;
  private Vector2 offset = Vector2.zero;
  private bool on = false;
  //public bool On
  //{
  //  set { on = value; }
  //}

  protected override void MakeAction()
  {
    on = true;
  }

  void Update()
  {
    if (on)
    {
      offset.y += Time.deltaTime * speed;
      mat.SetTextureOffset("_MainTex", offset);
    }
  }
}
