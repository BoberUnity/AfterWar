using UnityEngine;

public class SenderToStalker : MonoBehaviour 
{
  [SerializeField] private UIProgressBar progressBar = null;
  [SerializeField] private UISprite deadSprite = null;
  [SerializeField] private Indicator helthIndicator = null;
  [SerializeField] private ThingGUI bronButton = null;
  [SerializeField] private ArmoGUI[] armosGUI = new ArmoGUI[5];
  //[SerializeField] private UILabel fpsLabel = null;
  //private Character character = null;

  void Awake () 
  {
    Character character = GameObject.Find("Stalker").GetComponent<Character>();
    if (character != null)
    {
      character.Joystik = progressBar;
      character.DeadSprite = deadSprite;
      character.HelthIndicator = helthIndicator;
      character.BronButton = bronButton;
      character.ArmosGUI = armosGUI;
    }
    else Debug.LogWarning("Character was not found!");
	}

  //void Start()
  //{
  //  fpsLabel.enabled = character.Controller.ShowFps;
  //}
}
