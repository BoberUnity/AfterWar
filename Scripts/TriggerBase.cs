/*using UnityEngine;

public class TriggerBase : MonoBehaviour 
{
  [SerializeField] protected Character character = null;
  
	// Use this for initialization
	void Start ()
	{
    character.TriggerEnter += CheckThis;
	}

  void OnDestroy()
  {
    character.TriggerEnter -= CheckThis;
  }
  //Проверяем в этот ли триггер зашёл перс
  private void CheckThis(string tName)
  {
    if (tName == gameObject.name)
      TriggerEnter();
  }

  protected virtual void TriggerEnter() 
  {
	  
	}
}*/
