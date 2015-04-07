using UnityEngine;
using System.Collections;

public class MainMenu_GUI : MonoBehaviour {

	public CanvasGroup anchor_MainMenu;
	public CanvasGroup anchor_Intro;

	void Start()
	{
		//anchor_Intro.SetActive (false);
		anchor_Intro.alpha = 0;
		//anchor_MainMenu.SetActive (true);
		anchor_MainMenu.alpha = 1;
	}

	public void LoadIntro(){		
		//anchor_MainMenu.SetActive (false);
		anchor_MainMenu.GetComponent<CanvasGroup> ().alpha = 0;
		//anchor_Intro.SetActive (true);
		anchor_Intro.GetComponent<CanvasGroup> ().alpha = 1;
	}
	
	public void StartGame(){
		Application.LoadLevel(1);
	}

	public void QuitGame(){
		Application.Quit ();
	}

}
