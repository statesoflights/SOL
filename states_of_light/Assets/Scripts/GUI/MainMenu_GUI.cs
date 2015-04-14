using UnityEngine;
using System.Collections;

public class MainMenu_GUI : MonoBehaviour {

	public CanvasGroup anchor_MainMenu;
	public CanvasGroup anchor_Intro;
    public CanvasGroup anchor_Logo;

	void Start()
    {
		anchor_Intro.alpha = 0;
		anchor_MainMenu.alpha = 1;
        //anchor_Logo.alpha = 0;
        //StartCoroutine("FadeIn", anchor_Logo);
	}
    IEnumerator FadeIn(CanvasGroup group)
    {
        float alpha = group.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 2F)
        {
            group.alpha = Mathf.Lerp(alpha, 1, t);
            yield return null;
        }
        StartCoroutine("FadeOut", group);
    }
    IEnumerator FadeOut(CanvasGroup group)
    {
        float alpha = group.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1F)
        {
            group.alpha = Mathf.Lerp(alpha, 0, t);
            yield return null;
        }
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
