using UnityEngine;
using System.Collections;

public class MainMenu_GUI : MonoBehaviour {

	public CanvasGroup anchor_MainMenu;
	public CanvasGroup anchor_Intro;

	void Start()
    {
		anchor_Intro.alpha = 0;
		anchor_MainMenu.alpha = 1;
	}

    public void LoadIntro()
    {
		anchor_MainMenu.alpha = 0;
		anchor_Intro.alpha = 1;
	}
	
	public void StartGame(){
		Application.LoadLevel(1);
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
