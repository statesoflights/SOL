using UnityEngine;
using System.Collections;

public class Trigger_EndLevel : MonoBehaviour {

    public int Level_to_Load;
    LevelController lc;

    void Awake()
    {
        lc = LevelController.instance;
    }

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
