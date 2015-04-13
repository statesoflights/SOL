using UnityEngine;
using System.Collections;

public class Trigger_EndLevel : MonoBehaviour {

    public int Level_to_Load;
    LevelController lc;

    void Awake()
    {
        lc = LevelController.instance;
    }

    //void Start()
    //{
    //    if (Level_to_Load < 0 || Level_to_Load> Application.levelCount)
    //    {
    //        if (Application.loadedLevel + 1 <= Application.levelCount)
    //            Level_to_Load = Application.loadedLevel + 1;
    //        else
    //            Level_to_Load = Application.loadedLevel;
    //    }
    //}

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
            Application.LoadLevel(Level_to_Load);
		}
	}

    public void LoadLevel()
    {
        Application.LoadLevel(Level_to_Load);
    }
}
