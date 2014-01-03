using UnityEngine;

public class SenderToStalker : MonoBehaviour 
{
  [SerializeField] private UIProgressBar progressBar = null;
  [SerializeField] private UISprite deadSprite = null;
  [SerializeField] private Indicator helthIndicator = null;
  [SerializeField] private ArmoGUI[] armosGUI = new ArmoGUI[5];

  void Awake () 
  {
    Character character = GameObject.Find("Stalker").GetComponent<Character>();
    if (character != null)
    {
      character.Joystik = progressBar;
      character.DeadSprite = deadSprite;
      character.HelthIndicator = helthIndicator;
      character.ArmosGUI = armosGUI;
    }
    else Debug.LogWarning("Character was not found!");
	}
}
